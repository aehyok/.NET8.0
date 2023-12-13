using RabbitMQ.Client;

namespace aehyok.RabbitMQ
{
    public interface IConnection
    {
        global::RabbitMQ.Client.IConnection CreateConnection();

        IModel CreateModel();
    }
}