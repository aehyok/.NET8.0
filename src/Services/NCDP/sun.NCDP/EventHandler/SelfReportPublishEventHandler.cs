using sun.NCDP.EventData;
using sun.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.EventHandler
{
    public class SelfReportPublishEventHandler : IEventHandler<SelfReportPublishEventData>
    {
        public Task HandleAsync(SelfReportPublishEventData @event)
        {
            Console.WriteLine("SelfReportPublishEventHandler");
            return Task.CompletedTask;
        }
    }
}
