namespace MotorRental.Domain.Interfaces
{
    public interface IMessagingService
    {
        void SendMessage(string queueName, string message);
        string ReceiveMessage(string queueName);
    }
}
