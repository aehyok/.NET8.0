using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    /// <summary>
    /// 电商平台店铺
    /// </summary>
    public class CoupangStore: AuditedEntity
    {
        /// <summary>
        /// 店铺类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 店铺名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 店铺简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// AccessKey
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// AccessKey
        /// </summary>
        public string SecretKey { get; set;}

        /// <summary>
        /// 密钥过期时间
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 是否自动采集
        /// </summary>
        public bool IsAutoRun { get; set; }

        /// <summary>
        /// 状态（停用 正常）
        /// </summary>
        public string Status { get; set; }

        // 绑定时间，相当于创建时间吧
    }
}
