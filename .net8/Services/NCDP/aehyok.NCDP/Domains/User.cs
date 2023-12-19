using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Domains
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User : AuditedEntity
    {
        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(15)]
        public string Mobile { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(32)]
        public string RealName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [MaxLength(64)]
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(64)]
        public string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [MaxLength(256)]
        public string Password { get; set; }

        /// <summary>
        /// Password Salt
        /// </summary>
        [MaxLength(256)]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

        /// <summary>
        /// 性别
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [MaxLength(512)]
        public string Avatar { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [MaxLength(120)]
        public string Email { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 人口 Id
        /// </summary>
        public long PopulationId { get; set; }

        /// <summary>
        /// 签名url
        /// </summary>
        public string SignatureUrl { get; set; }

        /// <summary>
        /// 人口信息
        /// </summary>
        //public virtual Population Population { get; set; }

        /// <summary>
        /// 用户所有角色
        /// </summary>
        public virtual List<Role> Roles { get; set; }

        /// <summary>
        /// 角色用户关系数据
        /// </summary>
        public virtual List<UserRole> UserRoles { get; set; }
    }

    /// <summary>
    /// 性别
    /// </summary>
    public enum Gender
    {
        未知 = 0,
        男 = 1,
        女 = 2
    }
}
