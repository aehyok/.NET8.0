using aehyok.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Domains
{
    public class ScheduleTaskRecord:EntityBase
    {
        /// <summary>
        /// 任务 Id
        /// </summary>
        public long ScheduleTaskId { get; set; }

        /// <summary>
        /// 是否成功，没报错即成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 如果执行失败，错误消息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }

        /// <summary>
        /// 定时任务
        /// </summary>
        [ForeignKey(nameof(ScheduleTaskId))]
        public virtual ScheduleTask ScheduleTask { get; set; }
    }
}
