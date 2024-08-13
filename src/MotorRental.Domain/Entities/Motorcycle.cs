namespace MotorRental.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }

        public virtual Rental Rental { get; set; }
        public virtual MotorcycleNotification MotorcycleNotification { get; set; }
    }
}
