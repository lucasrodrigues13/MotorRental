using MotorRental.Domain.Constants;
using MotorRental.Domain.Interfaces;
using RabbitMQ.Client;
using System.Text;

namespace MotorRental.Infrastructure.ExternalServices
{
    public class RabbitMqService : IMessagingService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly MotorcycleNotificationQueueSettings _queueSettings;

        public RabbitMqService(MotorcycleNotificationQueueSettings queueSettings)
        {
            _queueSettings = queueSettings;

            var factory = new ConnectionFactory()
            {
                HostName = _queueSettings.HostName,
                UserName = _queueSettings.UserName,
                Password = _queueSettings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage(string queueName, string message)
        {
            _channel.QueueDeclare(queue: _queueSettings.QueueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: _queueSettings.QueueName,
                                 basicProperties: null,
                                 body: body);
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
