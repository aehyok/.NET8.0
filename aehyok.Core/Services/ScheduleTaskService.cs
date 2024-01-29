using aehyok.Core.Domains;
using aehyok.Core.Dtos;
using aehyok.Core.Schedule;
using aehyok.EntityFrameworkCore.Repository;
using aehyok.Infrastructure;
using aehyok.Infrastructure.TypeFinders;
using aehyok.Infrastructure.Utils;
using aehyok.Redis;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace aehyok.Core.Services
{
    public class ScheduleTaskService(DbContext dbContext, IMapper mapper, IServiceScopeFactory scopeFactory,IRedisService redisService) : ServiceBase<ScheduleTask>(dbContext, mapper), IScheduleTaskService, IScopedDependency
    {
        public async Task InitializeAsync()
        {
            var cronType = typeof(CronScheduleService);

            foreach (var cronService in TypeFinders.SearchTypes(cronType, TypeFinders.TypeClassification.Class))
            {
                var code = cronService.FullName;

                var cacheKey = CoreRedisConstants.ScheduleTaskCache.Format(code);

                // 从 Redis 中删除缓存
                await redisService.DeleteAsync(CoreRedisConstants.ScheduleTaskCache.Format(cacheKey));

                var exists = await this.GetAsync(a => a.Code == code);


                if (exists is null)
                {
                    var name = DocsHelper.GetTypeComments(cronService.Assembly.GetName().Name, cronService);

                    exists = new ScheduleTask
                    {
                        Code = code,
                        IsEnable = true,
                        Name = name.IsNullOrEmpty() ? code : name
                    };

                    // 将任务插入数据库中
                    await this.InsertAsync(exists);
                }


                var cacheValue = this.Mapper.Map<ScheduleTaskExecuteDto>(exists);

                // 将任务添加到 Redis 中
                await redisService.SetAsync(cacheKey, cacheValue);
            }
        }

        public async Task UpdateScheduleTaskStatusAsync(long scheduleTaskId,bool IsEnable, string cronExpression, DateTime nextExecuteTime)
        {
            var scheduleTask = await this.GetAsync(a => a.Id == scheduleTaskId);
            scheduleTask.IsEnable = IsEnable;
            scheduleTask.Expression = cronExpression;
            scheduleTask.NextExecuteTime = nextExecuteTime;

            await this.UpdateAsync(scheduleTask);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ScheduleTask task)
        {
            await base.UpdateAsync(task);

            var cacheValue = this.Mapper.Map<ScheduleTaskExecuteDto>(task);

            // 将任务添加到 Redis 中
            await redisService.SetAsync(CoreRedisConstants.ScheduleTaskCache.Format(task.Code), cacheValue);
        }
    }
}
