using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using ContainerService.models;
using System.Text.Json;
using System.Timers;
using ContainerService.services;

namespace ContainerService.service
{
    public class ReceiveService : IReceiveService
    {
        List<ContainerDto> containers;
        ContainersService containersService;
        public ReceiveService() 
        {
            containers = new List<ContainerDto>();
            containersService = new ContainersService();
            ReceiveStop();
        }

        public void ReceiveStop()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://sykzyrtg:Yqs1ZCUi-uxMZTWgae9mVnDWUjxVVB61@goose.rmq2.cloudamqp.com/sykzyrtg"),
            };
            IConnection conn = factory.CreateConnection();
            IModel channel = conn.CreateModel();
            channel.QueueDeclare(queue: "stop",
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

                containersService.StopContainer(dto.ContainerName);


            };
            channel.BasicConsume(queue: "stop",
                                    autoAck: true,
                                    consumer: consumer);
        }

    }
}
