using sun.Core.Domains;
using sun.Infrastructure.Dtos;

namespace sun.Basic.Dtos
{
    public class PermissionDto : DtoBase
    {
        /// <summary>
        /// 权限所属对象编号
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 菜单排序
        /// </summary>
        public int MenuOrder { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public long MenuId { get; set; }

        /// <summary>
        /// 权限数据范围
        /// </summary>
        public DataRange DataRange { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 父级菜单 Id
        /// </summary>
        public long MenuParentId { get; set; }

        /// <summary>
        /// 菜单 Url
        /// </summary>
        public string MenuUrl { get; set; }

        /// <summary>
        /// 菜单类型
        /// </summary>
        public MenuType MenuType { get; set; }

        /// <summary>
        /// 是否拥有权限
        /// </summary>
        public bool HasPermission { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public List<PermissionDto> Children { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public List<PermissionDto> Operations { get; set; }
    }
}
