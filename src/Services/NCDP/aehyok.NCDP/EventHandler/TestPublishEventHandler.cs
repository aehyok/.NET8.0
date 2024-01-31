using aehyok.NCDP.EventData;
using aehyok.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.EventHandler
{
    public class TestPublishEventHandler : IEventHandler<SelfReportPublishEventData>
    {
        public Task HandleAsync(SelfReportPublishEventData @event)
        {
            Console.WriteLine($"TestPublishEventHandler: {@event.TaskId}");
            return Task.CompletedTask;
        }
    }
}
