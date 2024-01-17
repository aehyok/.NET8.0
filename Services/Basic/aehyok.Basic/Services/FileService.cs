using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.FileStroage;
using aehyok.Infrastructure.SnowFlake;
using aehyok.Infrastructure.Utils;
using aehyok.Infrastructure.Video;
using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using File = aehyok.Basic.Domains.File;

namespace aehyok.Basic.Services
{
    /// <summary>
    /// 文件服务
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="mapper"></param>
    /// <param name="storageFactory"></param>
    /// <param name="contentTypeProvider"></param>
    public class FileService(DbContext dbContext, IMapper mapper, IFileStorageFactory storageFactory, IContentTypeProvider contentTypeProvider) : ServiceBase<File>(dbContext, mapper), IFileService, IScopedDependency
    {
        public Task<byte[]> GetContentAsync(string url)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetContentAsync(File file)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> GetContentAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<File> GetFileByUrlAsync(string path)
        {
            throw new NotImplementedException();
        }

        public Task<List<File>> GetFilesByIds(string ids)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTempFilePathAsync(File file)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTempFilePathAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTempFilePathAsync(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<File> UploadAsync(Stream stream, string originName, long originalFileId = 0, bool transcode = true)
        {
            var bytes = new byte[stream.Length];
            stream.Position = 0;
            await stream.ReadAsync(bytes, 0, bytes.Length);
            return await UploadAsync(bytes, originName, originalFileId, transcode);
        }

        public async Task<File> UploadAsync(byte[] bytes, string originName, long originalFileId = 0, bool transcode = true)
        {
            var storage = storageFactory.GetStorage();
            var file = new Domains.File
            {
                Size = bytes.Length,
                StorageType = storage.StorageType,
                Extension = Path.GetExtension(originName),
                Name = originName,
                OriginalId = originalFileId,
            };

            // 是否需要计算文件 MD5
            using var hasher = HashAlgorithm.Create("MD5");
            var hash = BitConverter.ToString(hasher.ComputeHash(bytes)).Replace("-", "").ToLower();

            // 根据 Hash 值判断文件是否已存在，如果该文件已存在，直接返回该文件信息
            var exists = await GetAsync(a => a.Hash == hash);
            if (exists is not null)
            {
                return exists;
            }

            var relativePath = $"{DateTime.Now.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo)}/{file.Id}{file.Extension}";
            file.Path = relativePath;

            if (!contentTypeProvider.TryGetContentType(originName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var fileType = contentType.Split("/")[0] switch
            {
                "text" => FileType.文档,
                "image" => FileType.图片,
                "audio" => FileType.音频,
                "video" => FileType.视频,
                _ => FileType.其他
            };

            file.Hash = hash;
            file.Type = fileType;

            if (fileType == FileType.视频 && transcode)
            {
                var tempFilePath = Path.Combine(App.GetTempPath(), $"{SnowFlake.Instance.NextId()}{Path.GetExtension(originName)}");

                using var writer = new FileStream(tempFilePath, FileMode.Create);
                await writer.WriteAsync(bytes, 0, bytes.Length);
                writer.Close();

                var isH264Codec = await VideoHelper.IsH264Codec(tempFilePath);

                // 如果视频不是 h264 编码，需要进行转码
                if (!isH264Codec)
                {
                    // 新建源文件对象，并上传源文件
                    var originalFile = await this.UploadAsync(bytes, originName, transcode: false);
                    originalFile.State = FileState.已禁用;

                    // 更新文件状态为禁用
                    await this.UpdateAsync(originalFile);

                    file.OriginalId = originalFile.Id;

                    // 发布转码事件
                    //this.eventPublisher.Publish(new VideoTranscodeEvent
                    //{
                    //    OriginalId = originalFile.Id,
                    //    TargetId = file.Id,
                    //});

                    //// 将视频的路径指向转码中视频的路径
                    //file.Path = App.Options<VideoTranscodeOptions>().DefaultVideoPath ?? "";
                    file.Url = $"{storage.GetAbsolutePath(relativePath)}?type=video";
                    file.State = FileState.转码中;
                }

                // 删除临时文件
                System.IO.File.Delete(tempFilePath);
            }

            if (file.Url.IsNullOrEmpty())
            {
                var url = await storage.UploadAsync(bytes, relativePath);
                if (fileType == FileType.视频)
                {
                    var fileTypeString = GetFileTypeString(fileType);
                    file.Url = $"{url}?type={fileTypeString}";
                }
                else
                {
                    file.Url = url;
                }

                file.State = FileState.正常;
            }

            file.ContentType = contentType;

            return await this.InsertAsync(file);
        }

        public Task<File> UploadAsync(string localPath, string originName = "", long originalFileId = 0, bool transcode = true)
        {
            throw new NotImplementedException();
        }

        public Task<File> UploadFromUrlAsync(string url, string originName = "")
        {
            throw new NotImplementedException();
        }

        private static string GetFileTypeString(FileType type)
        {
            return type switch
            {
                FileType.图片 => "image",
                FileType.视频 => "video",
                FileType.音频 => "audio",
                FileType.文档 => "text",
                _ => ""
            };
        }
    }
}
