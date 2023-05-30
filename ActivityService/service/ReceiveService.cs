using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using ContainerService.models;
using System.Text.Json;
using System.Timers;

namespace ActivityService.service
{
    public class ReceiveService : IReceiveService
    {
        List<ContainerDto> containers;
        public ReceiveService() 
        {
            containers = new List<ContainerDto>();
            ReceiveCommand();
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

                ContainerDto dto1 = containers.Find(x => x.ContainerName == dto.ContainerName);

                if(dto1 != null)
                {
                    dto1.timer.Stop();
                    dto1.timer.Start();
                }
                else
                {
                    dto1 = new ContainerDto();
                    dto1.ContainerName = dto.ContainerName;
                    dto1.timer.Enabled = true;
                    containers.Add(dto1);
                }

            };
            channel.BasicConsume(queue: "command",
                                    autoAck: true,
                                    consumer: consumer);
        }

        private  void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
