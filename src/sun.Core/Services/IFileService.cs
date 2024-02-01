using sun.EntityFrameworkCore.Repository;
using File = sun.Core.Domains.File;

namespace sun.Core.Services
{
    /// <summary>
    /// 文件服务接口
    /// </summary>
    public interface IFileService : IServiceBase<File>
    {
        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<byte[]> GetContentAsync(string url);

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<byte[]> GetContentAsync(File file);

        /// <summary>
        /// 获取文件内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<byte[]> GetContentAsync(long id);

        /// <summary>
        /// 根据文件 Url 获取文件信息
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Task<File> GetFileByUrlAsync(string path);

        /// <summary>
        /// 根据文件对象获取文件在电脑本地的临时文件路径
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<string> GetTempFilePathAsync(File file);

        /// <summary>
        /// 根据文件 Id 获取文件在电脑本地的临时文件路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetTempFilePathAsync(long id);

        /// <summary>
        /// 根据文件 Url 获取文件在电脑本地的临时文件路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<string> GetTempFilePathAsync(string url);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="originName"></param>
        /// <param name="originalFileId"></param>
        /// <param name="transcode">是否转码</param>
        /// <returns></returns>
        Task<File> UploadAsync(Stream stream, string originName, long originalFileId = 0, bool transcode = true);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="originName"></param>
        /// <param name="originalFileId"></param>
        /// <param name="transcode">是否转码</param>
        /// <returns></returns>
        Task<File> UploadAsync(byte[] bytes, string originName, long originalFileId = 0, bool transcode = true);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">文件本地路径</param>
        /// <param name="originName">原始名称</param>
        /// <param name="originalFileId">关联文件 Id</param>
        /// <param name="transcode">是否转码</param>
        /// <returns></returns>
        Task<File> UploadAsync(string localPath, string originName = "", long originalFileId = 0, bool transcode = true);

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<List<File>> GetFilesByIds(string ids);
        Task<File> UploadFromUrlAsync(string url, string originName = "");
    }
}
