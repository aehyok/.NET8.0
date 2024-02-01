using sun.Core.Domains;
using sun.EntityFrameworkCore.Entities;
using sun.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    /// <summary>
    /// 用户 Token
    /// </summary>
    public class UserToken : AuditedEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        [MaxLength(256)]
        public string Token { get; set; }

        /// <summary>
        /// Token Hash 用于查询
        /// </summary>
        [MaxLength(32)]
        public string TokenHash { get; set; }

        /// <summary>
        /// Refresh Token
        /// </summary>
        [MaxLength(256)]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 获取 Token IP 地址
        /// </summary>
        [MaxLength(32)]
        public string IpAddress { get; set; } = string.Empty;

        /// <summary>
        /// 获取 Token UserAgent
        /// </summary>
        [MaxLength(1024)]
        public string UserAgent { get; set; } = string.Empty;

        /// <summary>
        /// 当前获取 Token 平台
        /// </summary>
        public PlatformType PlatformType { get; set; }

        /// <summary>
        /// 当前登录的角色编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 当前登录的区域编号
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 登录方式
        /// </summary>
        public LoginMethodType LoginMethodType { get; set; }

        /// <summary>
        /// Refresh Token 是否有效
        /// </summary>
        public bool RefreshTokenIsAvailable { get; set; }

        /// <summary>
        /// Token 所属用户
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// 登录方式
        /// </summary>
        public LoginType LoginType { get; set; }
    }

    public enum LoginType
    {
        /// <summary>
        /// 登录状态
        /// </summary>
        Login = 1,
        /// <summary>
        /// 登出状态
        /// </summary>
        logout = 2
    }
}

