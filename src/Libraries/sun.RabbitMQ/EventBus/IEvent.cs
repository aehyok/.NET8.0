using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.RabbitMQ.EventBus
{
    /// <summary>
    /// 事件传输实体的基础接口
    /// </summary>
    public interface IEvent
    {
        object Id { get; set; }
    }
}
