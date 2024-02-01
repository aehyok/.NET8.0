using sun.Infrastructure.Enums;
using sun.Infrastructure.Models;

namespace sun.Core.Dtos.Query
{
    public class UserQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long? RoleId { get; set; }

        /// <summary>
        /// 区域id
        /// </summary>
        public long? RegionId { get; set; }

        /// <summary>
        /// 区域编码
        /// </summary>
        //public string RegionCode { get; set; }

        /// <summary>
        /// 包含下级
        /// </summary>
        public bool IncludeChilds { get; set; } = true;

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? IsEnable { get; set; }

        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// 权限code
        /// </summary>
        //public string PermissionCode { get; set; }
    }
}
