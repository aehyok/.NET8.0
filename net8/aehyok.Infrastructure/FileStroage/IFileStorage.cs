using aehyok.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.FileStroage
{
    public interface IFileStorage
    {
        /// <summary>
        /// 存储类型
        /// </summary>
        FileStorageType StorageType { get; }

        string GetAbsolutePath(string relativePath);

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<byte[]> GetAsync(string key);

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> UploadAsync(byte[] bytes, string key);
    }
}
