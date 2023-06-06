using Docker.DotNet.Models;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SecurityService.models;
using System.Text;

namespace SecurityService.services
{
    public class MessageService : IMessageService
    {
        ConnectionFactory _factory;
        IConnection _conn;
        IModel _channel;
        public MessageService()
        {
            Console.WriteLine("about to connect to rabbit");

            _factory = new ConnectionFactory() { Uri = new Uri("amqps://sykzyrtg:Yqs1ZCUi-uxMZTWgae9mVnDWUjxVVB61@goose.rmq2.cloudamqp.com/sykzyrtg") };
            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: "stop",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }
        public bool EnqueueStop(string containerName)
        {
            var json = JsonConvert.SerializeObject(new ContainerDto() { ContainerName = containerName});
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "",
                                routingKey: "stop",
                                basicProperties: null,
                                body: body);
            return true;
        }

    }
}
