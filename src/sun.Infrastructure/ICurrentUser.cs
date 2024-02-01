using sun.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Infrastructure
{
    public interface ICurrentUser
    {
        /// <summary>
        /// 行政区域编号
        /// </summary>
        long RegionId { get; }

        /// <summary>
        /// 角色编号
        /// </summary>
        long RoleId { get; }

        /// <summary>
        /// 用户编号
        /// </summary>
        long UserId { get; }

        /// <summary>
        /// 人口信息 Id
        /// </summary>
        long PopulationId { get; }

        /// <summary>
        /// 是否通过认证
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// 当前 Token
        /// </summary>
        string Token { get; }

        /// <summary>
        /// UserToken Id
        /// </summary>
        long TokenId { get; }

        /// <summary>
        ///  所属系统Id
        /// </summary>
        long SystemId { get; set; }

        /// <summary>
        /// 所属租户Id
        /// </summary>
        long TenantId { get; set; }

        /// <summary>
        /// 所属平台
        /// </summary>
        PlatformType PlatformType { get; }
    }
}
