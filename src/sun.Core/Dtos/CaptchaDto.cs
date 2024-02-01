using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos
{
    /// <summary>
    /// 图形验证码
    /// </summary>
    public class CaptchaDto
    {
        /// <summary>
        /// 验证码(base64字符串)
        /// </summary>
        public string Captcha { get; set; }

        /// <summary>
        /// 验证码 Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 验证码过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }
    }
}
