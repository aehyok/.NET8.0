using sun.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Dtos
{
    public class PasswordLoginDto
    {
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 验证码 Key
        /// </summary>
        public string CaptchaKey { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        public PlatformType PlatformType { get; set; }
    }
}
