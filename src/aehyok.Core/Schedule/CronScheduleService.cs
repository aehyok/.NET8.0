using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Services;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure.Utils;
using aehyok.Redis;
using AutoMapper;
using Cronos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace aehyok.Core.Schedule
{
    /// <summary>
    /// 实现一个后台定时任务的抽象类
    /// </summary>
    public abstract class CronScheduleService(IServiceScopeFactory serviceFactory) : BackgroundService
    {
        /// <summary>
        /// 定时表达式
        /// </summary>
        protected abstract string Expression { get; set; }

        /// <summary>
        /// 是否单例运行
        /// 如果为 True 在部署多个节点时，使用 Redis 作为分布式锁，限制每次只有一个服务执行
        /// </summary>
        protected abstract bool Singleton { get; }

        /// <summary>
        /// 获取下一次任务执行时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetNextTime()
        {
            return CronExpression.Parse(Expression, CronFormat.IncludeSeconds).GetNextOccurrence(DateTime.UtcNow, TimeZoneInfo.Local) ?? DateTime.MinValue;
        }

        protected abstract Task ProcessAsync(CancellationToken cancellationToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var code = this.GetType().FullName;

            // CronFormat.IncludeSeconds 表达式中秒字段必须被指定
            var nextExcuteTime = GetNextTime();

            if (nextExcuteTime != DateTime.MinValue)
            {
                Console.WriteLine($"{code}任务下次执行时间:{nextExcuteTime.ToLocalTime()}");
            }

            //用于准确测量时间间隔
            Stopwatch stopwatch = new();

            while (!stoppingToken.IsCancellationRequested)
            {
                // 把这个作用域和服务放到循环内部，循环结束便进行释放
                using var scope = serviceFactory.CreateAsyncScope();

                var scheduleTaskService = scope.ServiceProvider.GetService<IScheduleTaskService>();
                var recordService = scope.ServiceProvider.GetService<IServiceBase<ScheduleTaskRecord>>();
                var redisService = scope.ServiceProvider.GetService<IRedisService>();
                var logger = scope.ServiceProvider.GetService<ILogger<CronScheduleService>>();
                var mapper = scope.ServiceProvider.GetService<IMapper>();

                var scheduleTask = await redisService.GetAsync<ScheduleTaskExecuteDto>(CoreRedisConstants.ScheduleTaskCache.Format(code));
                if (scheduleTask is null || !scheduleTask.IsEnable)
                {
                    // 延迟重新执行
                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                if(!scheduleTask.Expression.IsNullOrEmpty())
                {
                    this.Expression = scheduleTask.Expression;
                }

                if(scheduleTask.IsEnable)
                {
                    scheduleTask.NextExecuteTime = nextExcuteTime;
                }


                if (DateTimeOffset.UtcNow < nextExcuteTime)
                {
                    // 延迟重新执行
                    await Task.Delay(1000, stoppingToken);
                    continue;
                }

                var lockName = $"ScheduleTask:{code}.{nextExcuteTime}";
                var scheduleTaskRecord = new ScheduleTaskRecord();
                scheduleTaskRecord.ScheduleTaskId = scheduleTask.Id;
                try
                {
                    stopwatch.Restart();
                    scheduleTaskRecord.ExpressionTime = nextExcuteTime;
                    scheduleTaskRecord.ExecuteStartTime = DateTime.Now;
                    if (!Singleton)
                    {
                        // ConfigureAwait允许你配置异步等待的行为。如果你使用ConfigureAwait(false)，
                        // 则表示你不需要恢复到原始上下文，而是允许异步操作在任何上下文中继续执行。
                        // 这通常可以提高性能，因为避免了上下文切换的开销。
                        await ProcessAsync(stoppingToken).ConfigureAwait(false);
                    }
                    else
                    {
                        // 在那台服务器上获取到了锁，便在那台服务器上进行执行?
                        if(await redisService.SetAsync(lockName, null,TimeSpan.FromMinutes(1), CSRedis.RedisExistence.Nx))
                        {
                            await ProcessAsync(stoppingToken).ConfigureAwait(false);

                            scheduleTask.LastExecuteTime = scheduleTask.NextExecuteTime;
                        }
                        else
                        {
                            logger.LogInformation($"定时任务 {code} 执行时未获取到锁，本次放弃执行");
                            continue;
                        }
                    }

                    scheduleTask.LastExecuteTime = DateTime.Now;
                    scheduleTaskRecord.ExecuteEndTime = DateTime.Now;
                    scheduleTaskRecord.IsSuccess = true;
                    await recordService.InsertAsync(scheduleTaskRecord);

                }
                catch(Exception ex)
                {
                    scheduleTaskRecord.ExecuteEndTime = DateTime.Now;
                    scheduleTaskRecord.ErrorMessage = ex.Message;
                    scheduleTaskRecord.IsSuccess = false;
                    await recordService.InsertAsync(scheduleTaskRecord);
                    logger.LogError($"执行 {code} 任务发生错误");
                    logger.LogError(ex, ex.Message);
                }

                nextExcuteTime = GetNextTime();

                scheduleTask.Expression = this.Expression;
                if(nextExcuteTime != DateTime.MinValue)
                {
                    scheduleTask.NextExecuteTime = nextExcuteTime;
                }

                var schedule = await scheduleTaskService.GetAsync(item => item.Code == code);

                mapper.Map(scheduleTask, schedule);

                await scheduleTaskService.UpdateAsync(schedule);
            }
        }
    }
}
