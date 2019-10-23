using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace aehyok.Core.Picture
{
    public class ImageUtils
    {
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encode(string source)//Encoding encodeType, 
        {
            string encode = string.Empty;
            byte[] bytes = (Encoding.UTF8.GetBytes(source));//encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
        public static string ImgToBase64String(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string UrlEncode(string str)
        {
            string urlStr = System.Web.HttpUtility.UrlEncode(str);
            string base64Str = Base64Encode(urlStr);
            return urlStr;
        }
    }
}
