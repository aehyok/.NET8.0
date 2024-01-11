using aehyok.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class RolePermissionDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 菜单 Key
        /// </summary>
        public string MenuCode { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 子菜单列表
        /// </summary>
        public List<RolePermissionDto> Children { get; set; }

        /// <summary>
        /// 数据范围
        /// </summary>
        //public DataRange DataRange { get; set; }

        /// <summary>
        /// 是否拥有权限
        /// </summary>
        //public bool HasPermission { get; set; }
    }
}
