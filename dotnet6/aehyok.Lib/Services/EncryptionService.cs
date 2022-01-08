using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace aehyok.Lib.Services
{
    public class EncryptionService : IEncryptionService
    {
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;
        //  private readonly IMetaDataQuery mdService;

        public EncryptionService(IConfiguration configuration,
            // IMetaDataQuery mdService,
            IHttpContextAccessor httpContextAccessor)
        {
            // this.mdService = mdService;
            this.httpContextAccessor = httpContextAccessor;
            this.configuration = configuration;
        }


        public async Task<string> GetCodeByToken()
        {
            string token = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrWhiteSpace(token))
            {
                token = httpContextAccessor.HttpContext.Request.Headers["Token"];
            }

            //var tokenHash = CreateHash(token, "SHA1");
            //string code = await RedisHelper.GetAsync($"code_{tokenHash}");
            ////if (string.IsNullOrEmpty(code))
            ////{
            //// code = await this.mdService.GetCodeByToken(token);
            //if (!string.IsNullOrEmpty(code))
            //{
            //    await RedisHelper.SetAsync($"code_{tokenHash}", code, expireSeconds: 60 * 60 * 24);
            //}
            //// }
            //return code;
            return "";
        }

        public virtual string CreateSaltKey(int size)
        {
            using (var provider = new RNGCryptoServiceProvider())
            {
                var buffer = new byte[size];
                provider.GetBytes(buffer);

                return Convert.ToBase64String(buffer);
            }
        }

        public virtual string CreatePasswordHash(string password, string saltKey, string passwordFormat)
        {
            return CreateHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)), passwordFormat);
        }

        public virtual string CreateHash(byte[] data, string hashAlgorithm)
        {
            if (string.IsNullOrWhiteSpace(hashAlgorithm))
            {
                throw new ArgumentNullException(hashAlgorithm);
            }

            var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm);
            if (algorithm == null)
            {
                throw new Exception("Unrecognized hash name");
            }

            var hashByteArray = algorithm.ComputeHash(data);

            return BitConverter.ToString(hashByteArray).Replace("-", string.Empty);
        }
        public virtual string CreateHash(string source, string hashAlgorithm)
        {
            if (string.IsNullOrWhiteSpace(hashAlgorithm))
            {
                throw new ArgumentNullException(hashAlgorithm);
            }

            var algorithm = (HashAlgorithm)CryptoConfig.CreateFromName(hashAlgorithm);
            if (algorithm == null)
            {
                throw new Exception("Unrecognized hash name");
            }

            var data = Encoding.UTF8.GetBytes(source);
            var hasByteArray = algorithm.ComputeHash(data);

            return BitConverter.ToString(hasByteArray).Replace("-", string.Empty);
        }

        public virtual async Task<string> EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrWhiteSpace(plainText))
            {
                return plainText;
            }

            if (string.IsNullOrWhiteSpace(encryptionPrivateKey))
            {
                //encryptionPrivateKey = this.configuration.GetValue<string>("EncryptionKey");
            }

            var key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
            var iv = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

            var encryptedBinary = await EncryptTextToMemory(plainText, key, iv);
            return Convert.ToBase64String(encryptedBinary);
        }

        public virtual async Task<string> DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            if (string.IsNullOrWhiteSpace(cipherText))
            {
                return cipherText;
            }

            if (string.IsNullOrWhiteSpace(encryptionPrivateKey))
            {
                //encryptionPrivateKey = this.configuration.GetValue<string>("EncryptionKey");
            }

            var key = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(0, 16));
            var iv = Encoding.ASCII.GetBytes(encryptionPrivateKey.Substring(8, 8));

            var buffer = Convert.FromBase64String(cipherText);

            return await DecryptTextFromMemory(buffer, key, iv);
        }


        private async Task<byte[]> EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    var toEncrypt = Encoding.Unicode.GetBytes(data);
                    await cs.WriteAsync(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }

        private async Task<string> DecryptTextFromMemory(byte[] data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateDecryptor(key, iv), CryptoStreamMode.Read))
                {
                    using (var sr = new StreamReader(cs, Encoding.Unicode))
                    {
                        return await sr.ReadToEndAsync();
                    }
                }
            }
        }


    }
}
