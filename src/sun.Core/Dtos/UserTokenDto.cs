using sun.Infrastructure.Dtos;
using sun.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos
{
    public class UserTokenDto
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpirationDate { get; set; }
    }

    /// <summary>
    /// 用户登录Token
    /// </summary>
    public class UserTokenLogDto: DtoBase
    {
        /// <summary>
        /// 登录用户
        /// </summary>
        public string loginUser { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime loginAt { get; set; }

        /// <summary>
        /// 登录平台
        /// </summary>
        public PlatformType PlatformType { get; set; }

        /// <summary>
        /// 登录角色
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 登录角色所属区域
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户浏览器
        /// </summary>
        public string UserAgent { get; set; }
    }
}
