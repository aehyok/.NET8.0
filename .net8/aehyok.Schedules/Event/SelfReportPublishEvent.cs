using aehyok.RabbitMQ.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.Schedules.Event
{
    public class SelfReportPublishEvent : EventBase
    {

        public long TaskId { get; set; }
    }
}
