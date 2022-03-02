using aehyok.Base;
using AutoMapper.Configuration.Annotations;
using System;
using System.Collections.Generic;

namespace aehyok.Core.EntityFramework.MySql.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class BaseUser: EntityBase
    {
        //public int Id { get; set; }
        /// <summary>
        /// 用户账号，兼容微信id
        /// </summary>
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? NickName { get; set; }
        /// <summary>
        /// 男性1，女性2，未知0
        /// </summary>
        public int? Sex { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public string? RoleIds { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
