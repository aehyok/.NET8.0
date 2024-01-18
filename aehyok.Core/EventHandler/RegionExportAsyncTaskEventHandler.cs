using aehyok.Core.EventData;
using aehyok.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Core.EventHandler
{
    public class RegionExportAsyncTaskEventHandler : IEventHandler<AsyncTaskEventData>
    {
        public Task HandleAsync(AsyncTaskEventData @event)
        {
            if(@event.TaskCode == "RegionExport")
            {
                Console.WriteLine("RegionExportAsyncTaskEventHandler");
            }

            return Task.CompletedTask;
        }
    }
}
