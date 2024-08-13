using MotorRental.Domain.Constants;

namespace MotorRental.Infrastructure.ExternalServices
{
    public class MotorcycleNotificationQueueSettings
    {
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; } = RabbitMqConstants.MOTORCYCLE_NOTIFICATION_QUEUE_NAME;
    }
}
