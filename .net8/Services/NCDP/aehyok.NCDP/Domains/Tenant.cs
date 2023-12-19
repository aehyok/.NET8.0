using aehyok.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.Domains
{
    /// <summary>
    /// 系统租户表
    /// </summary>
    public class Tenant: AuditedEntity
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// App秘钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 所属系统Id
        /// </summary>
        public long SystemId { get; set; } = 0;
    }
}
