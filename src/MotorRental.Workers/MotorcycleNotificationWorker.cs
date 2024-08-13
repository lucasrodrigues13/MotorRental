using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.ExternalServices;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorRental.Workers
{
    public class MotorcycleNotificationWorker : BackgroundService
    {
        private readonly ILogger<MotorcycleNotificationWorker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly MotorcycleNotificationQueueSettings _queueSettings;
        private IConnection _connection;
        private IModel _channel;

        public MotorcycleNotificationWorker(ILogger<MotorcycleNotificationWorker> logger, IServiceScopeFactory serviceScopeFactory, MotorcycleNotificationQueueSettings queueSettings)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _queueSettings = queueSettings;
            InitializeRabbitMQ();
        }

        private void InitializeRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _queueSettings.HostName,
                UserName = _queueSettings.UserName,
                Password = _queueSettings.Password
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _logger.LogInformation("RabbitMQ connection and queue declared.");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waiting for new motorcycles.");

            var factory = new ConnectionFactory()
            {
                HostName = _queueSettings.HostName
            };

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var motorcycle = JsonConvert.DeserializeObject<Motorcycle>(message);

                _logger.LogInformation("Received message: {Message}", message);

                if (motorcycle.Year == 2024)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var motorcyleRepository = scope.ServiceProvider.GetRequiredService<IMotorcyleRepository>();
                        motorcycle.MotorcycleNotification = new MotorcycleNotification { Motorcycle = motorcycle };
                        await motorcyleRepository.UpdateAsync(motorcycle);
                    }
                    _logger.LogInformation("Motorcycle 2024 saved.");
                }
                else
                    _logger.LogInformation("Motorcycle not saved because it is not 2024.");

                _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            _channel.BasicConsume(queue: _queueSettings.QueueName, autoAck: false, consumer: consumer);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}