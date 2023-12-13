using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.RabbitMQ.EventBus
{
    public interface IEventHandler
    {

    }

    public interface IEventHandler<TEvent> : IEventHandler
    {
        Task HandleAsync(TEvent @event);
    }
}
