using sun.Infrastructure.Enums;
using sun.Infrastructure.Models;

namespace sun.Core.Dtos.Query
{
    public class RoleQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 角色状态，True 启用 False 禁用
        /// </summary>
        public bool? IsEnable { get; set; }

        ///// <summary>
        ///// 所属平台
        ///// </summary>
        //public PlatformType platformType { get; set; }

    }
}
