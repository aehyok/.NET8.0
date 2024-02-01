using RabbitMQ.Client;

namespace sun.RabbitMQ
{
    public interface IConnection
    {
        global::RabbitMQ.Client.IConnection CreateConnection();

        IModel CreateModel();
    }
}