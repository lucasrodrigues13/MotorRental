using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        public int Year { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }

        public Rental Rental { get; set; }
        public MotorcycleNotification MotorcycleNotification { get; set; }
    }
}
