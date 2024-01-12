using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Models;

namespace aehyok.Core.Dtos.Query
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
