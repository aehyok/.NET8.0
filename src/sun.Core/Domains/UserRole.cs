using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Domains
{
    /// <summary>
    /// 用户角色关联信息
    /// </summary>
    public class UserRole : AuditedEntity
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 所属行政区域编号
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 是否默认角色
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 行政区域
        /// </summary>
        public virtual Region Region { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// 角色信息
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
