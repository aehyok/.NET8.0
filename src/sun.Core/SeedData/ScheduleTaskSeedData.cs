using sun.Core.Domains;
using sun.Core.Services;
using sun.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace sun.Core.SeedData
{
    /// <summary>
    /// 初始化定时任务到数据库
    /// </summary>
    /// <param name="scopeFactory"></param>
    public class ScheduleTaskSeedData(IServiceScopeFactory scopeFactory) : ISeedData, ITransientDependency
    {
        /// <summary>
        /// 尽量在初始化数据之后再执行
        /// </summary>
        public int Order => 10;

        public string ConfigPath { get; set; } = null;

        public async Task ApplyAsync(SeedDataTask model)
        {
            using var scope = scopeFactory.CreateScope();
            var scheduleTaskService = scope.ServiceProvider.GetRequiredService<IScheduleTaskService>();

            await scheduleTaskService.InitializeAsync();
        }
    }
}
