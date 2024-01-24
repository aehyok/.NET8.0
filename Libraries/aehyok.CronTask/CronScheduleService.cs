using Cronos;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace aehyok.CronTask
{
    /// <summary>
    /// 实现一个后台定时任务的抽象类
    /// </summary>
    public abstract class CronScheduleService : BackgroundService
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
            return CronExpression.Parse(Expression, CronFormat.IncludeSeconds).GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local);
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

            //用于准确测量时间间隔
            Stopwatch stopwatch = new();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (DateTimeOffset.UtcNow < nextExcuteTime)
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
                    Console.WriteLine($"任务执行时间:{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    Console.WriteLine("任务执行完成");
                }
                catch
                {

                }

                nextExcuteTime = GetNextTime();
                if (nextExcuteTime.HasValue)
                {
                    Console.WriteLine($"任务下次执行时间:{nextExcuteTime.Value.ToLocalTime()}");
                }
            }
        }
    }
}
