using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Domains
{
    /// <summary>
    /// 权限表RoleMenu(角色下勾选的所有菜单)
    /// </summary>
    public class Permission : AuditedEntity
    {
        /// <summary>
        /// 权限所属对象编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 权限数据范围
        /// </summary>
        public DataRange DataRange { get; set; }

        /// <summary>
        /// 是否拥有权限
        /// </summary>
        public bool HasPermission { get; set; }

        /// <summary>
        /// 菜单
        /// </summary>
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public virtual Role Role { get; set; }
    }

    /// <summary>
    /// 数据范围
    /// </summary>
    public enum DataRange
    {
        全部 = 0,
        本级 = 1,
        本级及下级 = 2,
        本级及上级 = 3,
        本人 = 4,
    }
}
