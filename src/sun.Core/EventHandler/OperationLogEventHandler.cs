using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using sun.Core.EventData;
using sun.Core.Services;
using sun.RabbitMQ.EventBus;
using sun.Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.Core.EventHandler
{
    public class OperationLogEventHandler(IServiceScopeFactory scopeFactory): IEventHandler<OperationLogEventData>
    {
        public async Task HandleAsync(OperationLogEventData @event)
        {
            using var scope = scopeFactory.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<OperationLogEventHandler>>();
            logger.LogInformation("OperationLogEventHandler");
            var httpContextAccessor = scope.ServiceProvider.GetService<IHttpContextAccessor>();
            var operationLogService = scope.ServiceProvider.GetService<IOperationLogService>();
            await operationLogService.LogAsync(@event.Code, @event.Content, @event.Json, @event.IpAddress, @event.UserAgent, @event.UserId);
        }
    }
}
