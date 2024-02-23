using sun.RabbitMQ.EventBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace sun.RabbitMQ
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IConnection connection;
        private readonly ILogger<EventPublisher> logger;
        private readonly RabbitOptions options;
        
        private IModel publisherChannel;
        public EventPublisher(IConnection connection, ILogger<EventPublisher> logger, IOptions<RabbitOptions> options) 
        {
            this.connection = connection;
            this.logger = logger;
            this.options = options.Value;
            this.publisherChannel = CreateChannel();
        }

        public void Publish<TEvent>(TEvent message) where TEvent : IEvent
        {
            var eventName = message.GetType().FullName;
            var body = JsonSerializer.Serialize(message);

            this.publisherChannel.BasicPublish(this.options.ExchangeName, eventName, null, Encoding.UTF8.GetBytes(body));
        }

        private IModel CreateChannel ()
        {
            var channel = connection.CreateModel();

            //channel.ExchangeDeclare(this.options.ExchangeName, ExchangeType.Fanout, true);
            channel.ExchangeDeclare(this.options.ExchangeName, ExchangeType.Direct, true);

            return channel;
        }
    }
}
