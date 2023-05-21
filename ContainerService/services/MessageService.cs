using ContainerService.models;
using Docker.DotNet.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ContainerService.services
{
    public class MessageService : IMessageService
    {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        public MessageService()
        {
            Console.WriteLine("about to connect to rabbit");

            _factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672 };
            _factory.UserName = "guest";
            _factory.Password = "guest";
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "command",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }
        public bool EnqueueCommand(string containerName, string command)
        {
            var json = JsonConvert.SerializeObject(new ContainerDto() { ContainerName = containerName, command=command});
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "",
                                routingKey: "command",
                                basicProperties: null,
                                body: body);
            return true;
        }

    }
}
