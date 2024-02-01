using sun.Basic.Services;
using sun.Core.Domains;
using sun.Core.Dtos;
using sun.Core.EventData;
using sun.Core.Services;
using sun.Infrastructure.Options;
using sun.RabbitMQ.EventBus;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
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
    /// <summary>
    ///  区域导出异步任务事件处理
    /// </summary>
    /// <param name="scopeFactory"></param>
    /// <param name="logger"></param>
    public class RegionExportAsyncTaskEventHandler : IEventHandler<AsyncTaskEventData>
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly ILogger<RegionExportAsyncTaskEventHandler> logger;

        public RegionExportAsyncTaskEventHandler(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }

        public async Task HandleAsync(AsyncTaskEventData @event)
        {
            if (@event.TaskCode != "RegionExport")
            {
                return;
            }

            using var scope = scopeFactory.CreateScope();
            var asyncTaskService = scope.ServiceProvider.GetRequiredService<IAsyncTaskService>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<RegionExportAsyncTaskEventHandler>>();

            var task = await asyncTaskService.GetByIdAsync(@event.TaskId);
            if(task is null || task.State != AsyncTaskState.待处理)
            {
                logger.LogInformation($"AsyncTaskEvent 任务[{task.Id}] 已处理完成或正在处理中");
                return;
            }

            try
            {
                task.State = AsyncTaskState.处理中;
                await asyncTaskService.UpdateAsync(task);

                var regionService = scope.ServiceProvider.GetService<IRegionService>();

                var exportDto = task.GetData<RegionExportQueryDto>();

                var exportFile = await regionService.ExportAsync(exportDto);

                task.State = AsyncTaskState.处理完成;
                task.Result = JsonSerializer.Serialize(new 
                {
                    exportFile.Name,
                    exportFile.Url,
                }, JsonOptions.Default);

                await asyncTaskService.UpdateAsync(task);
            }
            catch(Exception ex)
            {
                task.State = AsyncTaskState.失败;
                task.ErrorMessage = ex.Message;
                await asyncTaskService.UpdateAsync(task);
                logger.LogError(ex, $"AsyncTaskEvent 任务[{task.Id}] 处理失败");
            }
        }
    }
}
