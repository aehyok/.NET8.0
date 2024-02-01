using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sun.RabbitMQ.EventBus
{
    /// <summary>
    /// RabbitMQ 发布接口
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// 暂时没定义RabbitMQ 消息的类型
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="message"></param>
        void Publish<TEvent>(TEvent message) where TEvent : IEvent;
    }
}
