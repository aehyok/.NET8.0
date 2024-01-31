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
        public static string TokenCaptcha = $"{currentAssembly}:Captcha:{{0}}";

        /// <summary>
        /// 用户Token缓存 Key
        /// </summary>
        public static string UserToken = $"{currentAssembly}:UserToken:{{0}}";

        /// <summary>
        /// 定时任务 Cache Key
        /// </summary>
        public static string ScheduleTaskCache = $"ScheduleTask:{{0}}";
    }
}
