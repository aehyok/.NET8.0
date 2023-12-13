using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.RabbitMQ.EventBus
{
    public class EventBase : IEvent
    {
        public object Id { get; set; } = Guid.NewGuid();

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
