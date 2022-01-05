using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using aehyok.Lib.MetaData.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace aehyok.Lib.Services
{
    public class RSA
    {
        /// <summary>
        /// RSA使用公钥对数据加密
        /// </summary>
        /// <param name="publicKey"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RSAEncrypt(string publicKey, string content)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            byte[] cipherbytes;
            rsa.FromXmlString(RSAPublicKey(publicKey));
            cipherbytes = rsa.Encrypt(Encoding.UTF8.GetBytes(content), false);
            return Convert.ToBase64String(cipherbytes);
        }
        /// <summary>    
        /// RSA公钥pem-->XML格式转换， 
        /// </summary>    
        /// <param name="publicKey">pem公钥</param>    
        /// <returns></returns>    
        public static string RSAPublicKey(string publicKey)
        {
            RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
            string XML = string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
            Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
            Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
            return XML;
        }
    }

    public class BasicSOInit
    {

        //E_NOT_INIT = -10000,// 模块未初始化
        //E_CERT_INVALID,     // -9999, 证书无效
        //E_CERT_EXPIRED,     // -9998, 证书过期
        //E_INVALID_VALUE,    // -9997, 无效参数值
        //E_NULL_OUTBUF,      // -9996, 供保存数据的空间是空指针
        //E_VERIFY_ERR,       // -9995, 初始化时传入的加密数据错误

        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        public static bool InitSO()
        {
            // ILogger<object> logger = ServiceLocator.Current.GetService<ILogger<object>>();
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cert/cloak_public.pem");
                string publicKey = File.ReadAllText(filePath);
                string randomStr = StrUtils.GetRandomString(8, false, true);
                string input = RSA.RSAEncrypt(publicKey, randomStr);
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] outputBytes = new byte[1024];
                // logger.LogInformation("开始初始化——————————SO");


                int res = Init(inputBytes, outputBytes, 1024);
                // string output = Encoding.ASCII.GetString(outputBytes).Trim('\u0000');
                if (res < 0)
                {
                    // Log.CloseAndFlush();
                    string msg = string.Format("SO初始化失败({0})， 程序即将退出", res);
                    // logger.LogError(msg);

                    throw new Exception(msg);
                    // Process.GetCurrentProcess().Kill();
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("SO初始化失败,{0}", ex.Message);
                throw new Exception(msg);

            }

            return true;
        }

        // DllImport("mydll.dll",CharSet = CharSet.None, CallingConvention = CallingConvention.Cdecl)]
        [DllImport("/usr/lib64/libcloak.so", EntryPoint = "init_module")]
        private static extern int Init(byte[] in_code, byte[] out_code, int out_size);
    }


    public class BasicSO
    {
        private static ILogger logger;
        private static IConfiguration configuration;
        private static bool isOpen = true;
        static BasicSO()
        {

            //logger = ServiceLocator.Current.GetService<ILogger<object>>();
            //configuration = ServiceLocator.Current.GetService<IConfiguration>();
            //isOpen = !(configuration.GetValue<string>("OpenSO") == "0");

        }



        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plain"></param>
        /// <returns></returns>
        public static string Encrypt(string plain)
        {
            if (!File.Exists("/usr/lib64/libcloak.so"))
            {
                return $"[加密未安装]{plain}";
            }

            if (string.IsNullOrEmpty(plain) || !isOpen)
            {
                return plain;
            }
            byte[] inputBytes = Encoding.UTF8.GetBytes(plain);
            byte[] outputBytes = new byte[2048];
            int res = EnCode(inputBytes, inputBytes.Length, outputBytes, 2048);
            string output = Encoding.UTF8.GetString(outputBytes).Trim('\u0000');

            if (res < 0)
            {
                string msg = "发生错误（" + res.ToString() + "）";
                if (res == -9998)
                {
                    msg += "，服务器有效期已过期，请联系管理员申请重新注册";
                }
                else if (res == -9999)
                {
                    msg += "，证书无效";
                }
                return $"[加密失败]{plain}{msg}";
            }
            return output;
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        public static string Decrypt(string encrypted)
        {
            if (!File.Exists("/usr/lib64/libcloak.so"))
            {
                return $"[解密未安装]{encrypted}";
            }

            if (string.IsNullOrEmpty(encrypted) || !isOpen)
            {
                return encrypted;
            }

            byte[] inputBytes = Encoding.UTF8.GetBytes(encrypted);
            byte[] outputBytes = new byte[2048];
            int res = DeCode(inputBytes, outputBytes, 2048);
            string output = Encoding.UTF8.GetString(outputBytes).Trim('\u0000');
            if (res < 0)
            {
                string msg = "发生错误（" + res.ToString() + "）";
                if (res == -9998)
                {
                    msg += "，服务器有效期已过期，请联系管理员申请重新注册";
                }
                else if (res == -9999)
                {
                    msg += "，证书无效";
                }
                return $"[解密失败]{encrypted}{msg}";
            }
            return output;
        }




        [DllImport("/usr/lib64/libcloak.so", EntryPoint = "encrypt_string")]
        private static extern int EnCode(byte[] plain, int plain_len, byte[] encrypted_buf, int buf_size);

        [DllImport("/usr/lib64/libcloak.so", EntryPoint = "decrypt_string")]
        private static extern int DeCode(byte[] encrypted, byte[] plain, int buf_size);

    }
}
