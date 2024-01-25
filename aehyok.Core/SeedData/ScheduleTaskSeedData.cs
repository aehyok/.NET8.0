using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.SeedData
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
