using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core
{
    public class CoreRedisConstants
    {
        private static string currentAssembly = Assembly.GetExecutingAssembly().GetName().Name;

        /// <summary>
        /// 图片验证码缓存 Key
        /// </summary>
        public static string CAPTCHA_CACHE_KEY_PATTERN = $"{currentAssembly}:Captcha:{{0}}";

        /// <summary>
        /// 用户Token缓存 Key
        /// </summary>
        public static string USER_TOKEN_CACHE_KEY_PATTERN = $"{currentAssembly}:UserToken:{{0}}";
    }
}
