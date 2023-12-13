using RabbitMQ.Client;
using System.Text;

namespace aehyok.RabbitMQ
{
    public class HelloWorld
    {
        public static void Run()
        {
            Console.WriteLine("Hello, World!");

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5566,
                DispatchConsumersAsync = true,
                UserName = "lqm",
                Password = "sunlight",
                VirtualHost = "lqm_virtual"  //虚拟主机，要在服务器上创建，如果不创建，默认使用"/"，但是不建议使用"/"，因为"/"会和默认的vhost冲突
            };

            // 使用工厂创建连接
            using var connection = factory.CreateConnection("aehyok");

            // 创建信道
            using var channel = connection.CreateModel();

            // https://juejin.cn/post/6844903935048679437
            // durable: 是否持久化 发布消息和订阅消息的队列要保持一致，不一致会出现问题
            // exclusive: 是否排他,仅创建者可以使用的私有队列，断开后自动删除
            // autoDelete : 当所有消费客户端连接断开后，是否自动删除队列
            channel.QueueDeclare(queue: "akqueue",
                                 durable: false, 
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);


            foreach (var index in Enumerable.Range(0, 100))
            {
                var message = $"Hello, RabbitMQ! {index}";
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: string.Empty, routingKey: "akqueue", null, body);
            }

            channel.Close();

            connection.Close();

            Console.WriteLine("Hello, End!");

            Console.ReadLine();


        }
    }
}
