using aehyok.Core.Domains;
using aehyok.Core.Services;
using aehyok.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace aehyok.Core.HostedServices
{
    /// <summary>
    /// 应该为所有种子数据设置一个版本号，以避免每次启动的时候执行
    /// </summary>
    public class ExecuteSeedDataService(IServiceScopeFactory scopeFactory,IServiceProvider services,ILogger<ExecuteSeedDataService> logger, IConfiguration configuration) : BackgroundService
    {
        /// <summary>
        /// 当前配置文件的路径
        /// </summary>
        private string currentConfigPath = "";

        /// <summary>
        /// 当前任务是否需要执行
        /// </summary>
        private bool isExecute = false;

        /// <summary>
        /// 更新定时任务记录
        /// </summary>  
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task UpdateCronTask(ScheduleTask model)
        {
            using var scope = scopeFactory.CreateScope();
            var cronTaskCoreService = scope.ServiceProvider.GetRequiredService<ICronTaskCoreService>();

            await cronTaskCoreService.UpdateAsync(model);
        }

        /// <summary>
        /// 获取任务是否需要执行的状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        private void GetExecuteStatus(ScheduleTask model)
        {
            if (string.IsNullOrEmpty(currentConfigPath))
            {
                isExecute = true;
            }
            var file = new FileInfo(currentConfigPath);

            if (model.LastWriteTime < file.LastWriteTime)
            {
                isExecute = true;
                model.LastWriteTime = file.LastWriteTime;
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation($"开始执行种子数据更新任务");

            var stopwatch = new Stopwatch();

            stopwatch.Start();
            using var scope = scopeFactory.CreateScope();
            //var redisDatabaseProvider = scope.ServiceProvider.GetService<IRedisDatabaseProvider>();
            //var seedDataSection = configuration.GetSection("SeedData");


            //using var scope = services.CreateScope();
            var redisDatabaseProvider = scope.ServiceProvider.GetRequiredService<IRedisService>();

            var seedInstants = scope.ServiceProvider.GetServices<ISeedData>();

            var cronTaskCoreService = scope.ServiceProvider.GetRequiredService<ICronTaskCoreService>();

            var list = await cronTaskCoreService.GetListAsync();

            foreach (var seed in seedInstants.OrderBy(a => a.Order))
            {
                var taskName = seed.GetType().Name;
                currentConfigPath = seed.ConfigPath;
                var taskId = Guid.NewGuid().ToString();
               
                var model = list.FirstOrDefault(item => item.TaskName == taskName);

                try
                {
                    //判断该任务是否启用，并且是否已同步到数据库，如果没有同步，则要写入数据库
                    if (model is not null && !model.IsEnable)
                    {
                        break; //如果任务已经禁用，则不再执行
                    }

                    if(model is null)
                    {
                        model = await cronTaskCoreService.InsertAsync(new ScheduleTask
                        {
                            TaskName = taskName,
                            IsEnable = true,
                            
                        });

                    }

                    GetExecuteStatus(model);

                    if (isExecute)
                    {
                        logger.LogInformation($"开始执行[{taskName}]");

                        if (await redisDatabaseProvider.SetAsync(taskName, taskId, TimeSpan.FromMinutes(5), CSRedis.RedisExistence.Nx))
                        {
                            await seed.ApplyAsync(model, UpdateCronTask);
                        }

                        logger.LogInformation($"[{taskName}]执行完成");
                    }

                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                }
                finally
                {
                    //await redisDatabaseProvider.LockReleaseAsync(taskName, taskId);
                    await UpdateCronTask(model);
                    await redisDatabaseProvider.DeleteAsync(taskName);
                }
            }

            stopwatch.Stop();
            logger.LogInformation($"种子数据更新任务执行完成，耗时:{stopwatch.ElapsedMilliseconds}ms");
        }
    }
}
