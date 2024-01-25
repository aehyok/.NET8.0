using aehyok.EntityFrameworkCore.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Domains
{
    /// <summary>
    /// 种子数据更新任务
    /// </summary>
    public class SeedDataTask:EntityBase
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

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }

        /// <summary>
        /// 任务执行状态
        /// </summary>
        public ExecuteStatus ExecuteStatus { get; set; }

        /// <summary>
        /// 配置文件地址
        /// </summary>
        public string ConfigPath { get; set; }
    }
}

/// <summary>
/// 执行状态
/// </summary>
public enum ExecuteStatus
{
    成功 = 1,
    失败 = 2
}