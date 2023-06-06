using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Timers;
using SecurityService.services;
using SecurityService.models;

namespace SecurityService.service
{
    public class ReceiveService : IReceiveService
    {
        DetectionSystem _detectionSystem;
        IMessageService _messageService;
        public ReceiveService() 
        {
            ReceiveCommand();
            _detectionSystem = new DetectionSystem();
            _messageService = new MessageService();
        }

        public void ReceiveCommand()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://sykzyrtg:Yqs1ZCUi-uxMZTWgae9mVnDWUjxVVB61@goose.rmq2.cloudamqp.com/sykzyrtg"),
            };
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "command",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                ContainerDto dto =  JsonSerializer.Deserialize<ContainerDto>(message);

                if (_detectionSystem.ControllCommand(dto.command))
                {
                    _messageService.EnqueueStop(dto.ContainerName);
                }

            };
            channel.BasicConsume(queue: "command",
                                    autoAck: true,
                                    consumer: consumer);
        }

    }
}
