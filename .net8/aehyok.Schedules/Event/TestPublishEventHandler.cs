using aehyok.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Schedules.Event
{
    public class TestPublishEventHandler: IEventHandler<SelfReportPublishEvent>
    {
        public Task HandleAsync(SelfReportPublishEvent @event)
        {
            Console.WriteLine($"TestPublishEventHandler: {@event.TaskId}");
            return Task.CompletedTask;
        }
    }
}
