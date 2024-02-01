using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.RabbitMQ.EventBus
{
    /// <summary>
    /// 事件传递数据基类
    /// </summary>
    public class EventBase : IEvent
    {
        public object Id { get; set; } = Guid.NewGuid();

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
