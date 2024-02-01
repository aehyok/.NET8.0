using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.RabbitMQ.EventBus
{
    /// <summary>
    /// RabbitMQ订阅者接口
    /// </summary>
    public interface IEventSubscriber: IDisposable
    {
        void Subscribe(Type eventType, Type eventHandlerType);
    }
}
