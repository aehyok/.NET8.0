using aehyok.Infrastructure;
using aehyok.Infrastructure.Enums;
using aehyok.Infrastructure.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core
{

    public static class DvsClaimTypes
    {
        public const string RegionId = "DVS.RegionId";

        public const string UserId = "DVS.UserId";

        public const string RoleId = "DVS.RoleId";

        public const string Roles = "DVS.Roles";

        public const string Token = "DVS.Token";

        public const string PopulationId = "DVS.PopulationId";

        public const string TokenId = "DVS.TokenId";

        public const string PlatFormType = "DVS.PlatFormType";
    }


    /// <summary>
    /// 当前登录用户
    /// </summary>
    public class CurrentUser : ICurrentUser, ISingletonDependency
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// 当前用户区域 Id
        /// </summary>
        public long RegionId => this.FindClaimValue<long>(DvsClaimTypes.RegionId);

        /// <summary>
        /// 当前角色 Id
        /// </summary>
        public long RoleId => this.FindClaimValue<long>(DvsClaimTypes.RoleId);

        /// <summary>
        /// 当前登录用户 Id
        /// </summary>
        public long UserId => this.FindClaimValue<long>(DvsClaimTypes.UserId);

        /// <summary>
        /// 人口信息 Id
        /// </summary>
        //public long PopulationId => this.FindClaimValue<long>(DvsClaimTypes.PopulationId);

        /// <summary>
        /// 是否通过认证
        /// </summary>
        public bool IsAuthenticated => this.httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public string Token => this.FindClaimValue(DvsClaimTypes.Token);

        public long TokenId => this.FindClaimValue<long>(DvsClaimTypes.TokenId);

        public long SystemId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public long TenantId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public long PopulationId => throw new NotImplementedException();

        public PlatformType PlatformType => this.FindClaimValue<PlatformType>(DvsClaimTypes.PlatFormType);

        public virtual Claim FindClaim(string claimType)
        {
            return this.httpContextAccessor.HttpContext?.User?.FindFirst(claimType);
        }

        public virtual string FindClaimValue(string claimType)
        {
            return FindClaim(claimType)?.Value;
        }

        public virtual T FindClaimValue<T>(string claimType) where T : struct
        {
            var value = FindClaimValue(claimType);

            if (value == null)
            {
                return default;
            }

            return value.To<T>();
        }
    }
}
