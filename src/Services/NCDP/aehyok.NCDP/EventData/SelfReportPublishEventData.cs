using aehyok.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.NCDP.EventData
{
    public class SelfReportPublishEventData : EventBase
    {
        public long TaskId { get; set; }
    }
}
