using sun.Basic.Domains;
using sun.Core.Domains;
using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.Domains
{
    /// <summary>
    /// 填写审核状态
    /// </summary>
    public enum WriteStatus
    {
        待提交 = 1,
        待审核 = 2,
        审核通过 = 3,
        驳回 = 4,

        已填写 = 99  // 不需要审核
    }

    public class AutoRecord: AuditedEntity
    {
        /// <summary>
        /// 自动化任务Id
        /// </summary>
        public long AutoTaskId { get; set; }

        public AutoTask AutoTask { get; set; }

        /// <summary>
        /// 数据记录填报 区域Id
        /// </summary>
        public long RegionId { get; set; }

        public Region Region { get; set; }

        /// <summary>
        /// 关联的表单业务表字段Id
        /// </summary>
        public string FormBusinessId { get; set; }

        /// <summary>
        /// 填写状态
        /// </summary>
        public WriteStatus WriteStatus { get; set; }
    }
}
