using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.Dtos
{
    public class SeedDataTaskDto
    {
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// 最后一次执行时间
        /// </summary>
        public DateTime LastWriteTime { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public ExecuteStatus ExecuteStatus { get; set; }

        /// <summary>
        /// 配置文件
        /// </summary>
        public string ConfigPath { get; set; }
    }
}
