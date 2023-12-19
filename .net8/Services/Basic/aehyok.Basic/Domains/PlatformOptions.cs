using aehyok.EntityFramework.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Basic.Domains
{
    /// <summary>
    /// 租户平台配置项
    /// </summary>
    public class PlatformOptions : FullAuditedEntity<User>
    {
        public string SectionName => "Platform";

        /// <summary>
        /// 平台名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 平台 Logo
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 平台主题
        /// </summary>
        public string Theme { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 登录页背景
        /// </summary>
        public string LoginImage { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public long TenantId { get; set; }

        /// <summary>
        /// 所属租户
        /// </summary>
        public virtual Tenant Tenant { get; set; }
    }
}
