using sun.EntityFrameworkCore.Repository;
using sun.Infrastructure;
using sun.Infrastructure.Enums;
using sun.Infrastructure.Exceptions;
using sun.Infrastructure.FileStroage;
using sun.Infrastructure.SnowFlake;
using sun.Infrastructure.Utils;
using sun.Infrastructure.Video;
using AutoMapper;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using File = sun.Core.Domains.File;

namespace sun.Core.Services
{
    /// <summary>
    /// 文件服务
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="mapper"></param>
    /// <param name="storageFactory"></param>
    /// <param name="contentTypeProvider"></param>
    /// <param name="httpClientFactory"></param>
    public class FileService(DbContext dbContext, IMapper mapper, IFileStorageFactory storageFactory, IContentTypeProvider contentTypeProvider, IHttpClientFactory httpClientFactory) : ServiceBase<File>(dbContext, mapper), IFileService, IScopedDependency
    {
        public Task<byte[]> GetContentAsync(string url)
        {
            var idStr = Regex.Match(url, "[0-9]{19}").Value;

            if (long.TryParse(idStr, out var id))
            {
                return GetContentAsync(id);
            }

            throw new ErrorCodeException(-1, "文件未找到");
        }

        public async Task<byte[]> GetContentAsync(File file)
        {
            return await storageFactory.GetStorage().GetAsync(file.Path);
        }

        public async Task<byte[]> GetContentAsync(long id)
        {
            var file = await this.GetByIdAsync(id);
            return await GetContentAsync(file);
        }

        public Task<File> GetFileByUrlAsync(string path)
        {
            // 使用正则匹配 19 位雪花 Id
            var id = Regex.Match(path, "[0-9]{19}").Value;
            return this.GetByIdAsync(id);
        }

        public Task<List<File>> GetFilesByIds(string ids)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetTempFilePathAsync(File file)
        {
            // 判断本地是否有该文件，如果有则直接返回文件路径，没有则将文件下载到本地
            var tempPath = App.GetTempPath();
            var tempFilePath = Path.Combine(tempPath, $"{file.Id}{file.Extension}");

            if (System.IO.File.Exists(tempFilePath))
            {
                return tempFilePath;
            }

            // 如果是开发模式，则直接下载该文件到本地环境
            if (Debugger.IsAttached)
            {
                var client = httpClientFactory.CreateClient();
                var downloadBytes = await client.GetByteArrayAsync(file.Url);

                using var tempStream = new FileStream(tempFilePath, FileMode.Create);
                await tempStream.WriteAsync(downloadBytes);

                return tempFilePath;
            }

            var bytes = await this.GetContentAsync(file);
            using var stream = new FileStream(tempFilePath, FileMode.Create);
            await stream.WriteAsync(bytes);

            return tempFilePath;
        }

        public async Task<string> GetTempFilePathAsync(long id)
        {
            var file = await this.GetByIdAsync(id);
            return await GetTempFilePathAsync(file);
        }

        public async Task<string> GetTempFilePathAsync(string url)
        {
            var idStr = Regex.Match(url, "[0-9]{19}").Value;

            if (long.TryParse(idStr, out var id))
            {
                return await GetTempFilePathAsync(id);
            }

            throw new ErrorCodeException(-1, "文件未找到");
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
            var file = new File
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
