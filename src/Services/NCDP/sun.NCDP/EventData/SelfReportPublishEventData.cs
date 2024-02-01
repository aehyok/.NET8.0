using sun.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.NCDP.EventData
{
    public class SelfReportPublishEventData : EventBase
    {
        public long TaskId { get; set; }
    }
}
