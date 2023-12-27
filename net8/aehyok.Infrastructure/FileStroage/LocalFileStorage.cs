using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Exceptions;
using aehyok.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Infrastructure.FileStroage
{
    /// <summary>
    /// 本地文件存储
    /// </summary>
    public class LocalFileStorage(IOptionsSnapshot<StorageOptions> storageOptions, IOptionsSnapshot<CommonOptions> commonOptions) : IFileStorage, IScopedDependency
    {
        public FileStorageType StorageType => FileStorageType.Local;

        public async Task<string> UploadAsync(byte[] bytes, string key)
        {
            var fileSavePath = Path.Combine(GetStorageBaseDirectory(), key);
            Directory.CreateDirectory(Path.GetDirectoryName(fileSavePath));

            using var file = File.Create(fileSavePath);
            await file.WriteAsync(bytes);

            return GetAbsolutePath(key);
        }

        public async Task<byte[]> GetAsync(string key)
        {
            var filePath = Path.Combine(GetStorageBaseDirectory(), key);

            if (!File.Exists(filePath))
            {
                throw new ErrorCodeException(-1, $"文件[{filePath}]不存在");
            }

            return await File.ReadAllBytesAsync(filePath);
        }

        private string GetStorageBaseDirectory()
        {
            var basePath = storageOptions.Value.Path;

            if (basePath.IsNullOrEmpty())
            {
                return Path.Combine(AppContext.BaseDirectory, "uploads");
            }

            if (basePath.StartsWith("/"))
            {
                return basePath;
            }

            return Path.Combine(AppContext.BaseDirectory, basePath);
        }

        public virtual string GetAbsolutePath(string relativePath)
        {
            var host = commonOptions.Value.Host;

            if (host.IsNullOrEmpty())
            {
                return relativePath;
            }

            return $"{host}/static/{relativePath}";
        }
    }
}
