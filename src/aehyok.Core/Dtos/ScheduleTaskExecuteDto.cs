using aehyok.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.Dtos
{
    public class ScheduleTaskExecuteDto
    {
        /// <summary>
        /// 定时任务 Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Cron 表达式
        /// </summary>
        public string Expression { get; set; }

        /// <summary>
        /// 是否启用的状态
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 代码，默认为 Schedule 的类名
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 上次执行时间
        /// </summary>
        public DateTime LastExecuteTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 下一次执行时间
        /// </summary>
        public DateTime NextExecuteTime { get; set; }
    }
}
