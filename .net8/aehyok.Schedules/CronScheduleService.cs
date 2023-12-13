using Cronos;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Schedules
{
    /// <summary>
    /// 实现一个后台服务
    /// </summary>
    public abstract class CronScheduleService: BackgroundService
    {
        /// <summary>
        /// 定时表达式
        /// </summary>
        protected abstract string Expression { get; }

        /// <summary>
        /// 获取下一次任务执行时间
        /// </summary>
        /// <returns></returns>
        public DateTime? GetNextTime()
        {
            return  CronExpression.Parse(Expression, CronFormat.IncludeSeconds).GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local);
        }

        protected abstract Task ProcessAsync(CancellationToken cancellationToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // CronFormat.IncludeSeconds 表达式中秒字段必须被指定
            var nextExcuteTime = GetNextTime();

            if (nextExcuteTime.HasValue)
            {
                Console.WriteLine($"任务下次执行时间:{nextExcuteTime.Value.ToLocalTime()}");
            }


            ///用于准确测量时间间隔
            Stopwatch stopwatch = new Stopwatch();

            while (!stoppingToken.IsCancellationRequested)
            {
                if(DateTimeOffset.UtcNow < nextExcuteTime)
                {
                    // 延迟重新执行
                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                try
                {
                    stopwatch.Restart();
                    Console.WriteLine("开始执行任务");
                    await ProcessAsync(stoppingToken);
                    Console.WriteLine($"任务执行时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}");
                    Console.WriteLine("任务执行完成");
                }
                catch
                {

                }

                nextExcuteTime = GetNextTime();
                if(nextExcuteTime.HasValue)
                {
                    Console.WriteLine($"任务下次执行时间:{nextExcuteTime.Value.ToLocalTime()}");
                }
            }
        }
    }
}
