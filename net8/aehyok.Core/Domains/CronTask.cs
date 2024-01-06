using aehyok.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Domains
{
    /// <summary>
    /// 计划定时任务表
    /// </summary>
    public class ScheduleTask:EntityBase
    {
        /// <summary>
        /// 最后一次的文件修改时间
        /// </summary>
        public DateTime LastWriteTime { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; } = true;

    }
}


public enum TaskType
{
    /// <summary>
    /// 种子数据
    /// </summary>
    SeedData = 1,
}