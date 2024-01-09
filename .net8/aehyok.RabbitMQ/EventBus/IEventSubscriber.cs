﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.RabbitMQ.EventBus
{
    public interface IEventSubscriber: IDisposable
    {
        void Subscribe(Type eventType, Type eventHandlerType);
    }
}
