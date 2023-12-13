using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace aehyok.RabbitMQ
{
    public class FanoutCF: ICF
    {
        private readonly IConnection connection;
        public FanoutCF(IConnection connection)
        {
            this.connection = connection;
        }

        public void Publish()
        {
            // 创建信道
            using var channel = this.connection.CreateModel();

            // 参数autoDelete: false 默认为false，true表示当没有消费者的时候自动删除
            channel.ExchangeDeclare("sun", ExchangeType.Fanout,durable:true);

            foreach (var index in Enumerable.Range(0, 100))
            {
                var message = $"Hello, RabbitMQ! {index}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: "sun", routingKey: string.Empty, basicProperties: null, body: body);
            }

        }

        public void Subscrber()
        {
            var channel = this.connection.CreateModel();

            channel.QueueDeclare(queue: "sunlight", true, false, false);
            channel.QueueBind(queue: "sunlight", exchange: "sun", routingKey: string.Empty, arguments: null);

            // AsyncEventingBasicConsumer继承了 IBasicConsumer，则会在消息到达时自动推送，而无需主动请求，参考https://www.cnblogs.com/hsyzero/p/6297644.html

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"接收到消息：{message}");


                //// 如果消息处理成功，则发送确认
                channel.BasicAck(e.DeliveryTag, false);

                //// 如果消息处理失败，requeue设置为true，表示重新放回队列，如果设置为false，则表示丢弃该消息
                //channel.BasicReject(e.DeliveryTag, false);
                await Task.Yield();
            };

            // autoAck: true 表示自动把发送出去的消息设置为确认，然后从内存或者硬盘中删除，而不管消费者是否消费到了消息。
            // autoAck: false 表示手动确认
            channel.BasicConsume(queue: "sunlight",
                     autoAck: false,
                     consumer: consumer);
            
            //channel.Close();
            //connection.Close();
        }
    }
}
