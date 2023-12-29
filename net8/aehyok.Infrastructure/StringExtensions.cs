using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace aehyok.Infrastructure
{
    /// <summary>
    /// 字符串扩展方法
    /// </summary>
    public static class StringExtensions
    {

        /// <summary>
        /// 字符串格式化，String.Format() 方法的语法糖
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(this string source, params object[] args)
        {
            return string.Format(source, args);
        }

        /// <summary>
        /// 判断字符串是否为手机号码
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsMobile(this string source)
        {
            return Regex.IsMatch(source, @"^1\d{10}$");
        }

        /// <summary>
        /// 密码混淆
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string EncodePassword(this string password, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var src = Convert.FromBase64String(salt);
            var dst = new byte[src.Length + bytes.Length];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            byte[] encodeBytes = HashAlgorithm.Create("SHA256").ComputeHash(dst);

            return Convert.ToBase64String(encodeBytes);
        }

        /// <summary>
        /// 生成 Token
        /// </summary>
        /// <param name="username"></param>
        /// <param name="expirationDate"></param>
        /// <returns></returns>
        public static string GenerateToken(string userName, DateTimeOffset expirationDate)
        {
            var data = new byte[64];
            RandomNumberGenerator.Create().GetBytes(data);
            return Convert.ToBase64String(data);
        }
    }
}
