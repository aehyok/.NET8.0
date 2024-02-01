using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.EventData;
using sun.Core.Services;
using sun.Infrastructure.Options;
using sun.RabbitMQ.EventBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sun.Core.EventHandler
{
    public class RegionImportAsyncTaskEventHandler : IEventHandler<AsyncTaskEventData>
    {
        private readonly IServiceScopeFactory scopeFactory;

        public RegionImportAsyncTaskEventHandler(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task HandleAsync(AsyncTaskEventData @event)
        {
            if (@event.TaskCode != "RegionImportAsyncTaskEventHandler")
            {
                return;
            }

            using var scope = scopeFactory.CreateScope();
            var asyncTaskService = scope.ServiceProvider.GetService<IAsyncTaskService>();

            var logger = scope.ServiceProvider.GetService<ILogger<RegionImportAsyncTaskEventHandler>>();

            var task = await asyncTaskService.GetAsync(a => a.Id == @event.TaskId);
            if (task is null || task.State != AsyncTaskState.待处理)
            {
                logger.LogInformation($"AsyncTaskEvent 任务 [{task.Id}] 已处理完成或正在处理中");
                return;
            }

            try
            {
                // 修改任务状态为处理中
                task.State = AsyncTaskState.处理中;
                await asyncTaskService.UpdateAsync(task);

                var userService = scope.ServiceProvider.GetService<IUserService>();

                var importModel = task.GetData<ImportUserModel>();

                var result = await userService.ImportAsync(importModel.Url, importModel.UserType, task.CreatedBy.Value);
                task.State = AsyncTaskState.处理完成;
                task.Result = JsonSerializer.Serialize(result, JsonOptions.Default);

                await asyncTaskService.UpdateAsync(task);
            }
            catch (Exception ex)
            {
                task.State = AsyncTaskState.失败;
                task.ErrorMessage = ex.Message;
                await asyncTaskService.UpdateAsync(task);
                logger.LogError(ex, $"执行导入工作人员用户事件发生错误");
            }
        }
    }
}
