using sun.Basic.Domains;
using sun.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.Domains
{
    /// <summary>
    /// 自动化任务表
    /// </summary>
    public class AutoTask: AuditedEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间，精确到秒
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDateTime { get; set; }

        /// <summary>
        /// 当前任务发布所属区域
        /// </summary>
        public long RegionId { get; set; }

        /// <summary>
        /// 发布区域Id(单个)
        /// </summary>
        public long PublishRegionId { get; set; }

        /// <summary>
        /// 区域发布范围（多个）
        /// </summary>
        public string PublishRegionIds { get; set; }

        /// <summary>
        /// 是否允许填报多次
        /// </summary>
        public bool IsMore { get; set; }

        /// <summary>
        /// 填报附件
        /// </summary>
        public string AttachmentIds { get; set; }

        /// <summary>
        /// 是否推送消息
        /// </summary>
        public bool IsPushMessage { get; set; }
    }
}
