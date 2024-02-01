using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Basic.Domains
{
    /// <summary>
    /// 系统配置参数项
    /// </summary>
    public class Options : AuditedEntity
    {
        /// <summary>
        /// 键
        /// </summary>
        [MaxLength(1024)]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [Column(TypeName = "text")]
        public string Value { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(1024)]
        public string Remark { get; set; }
        
        ///// <summary>
        ///// 租户Id
        ///// </summary>
        //public long TenantId { get; set; }

        ///// <summary>
        ///// 所属租户
        ///// </summary>
        //public virtual Tenant Tenant { get; set; }

        /// <summary>
        /// 所属系统Id
        /// </summary>
        public long SystemId { get; set; } = 0;


    }
}
