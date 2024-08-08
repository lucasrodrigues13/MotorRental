using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class Motorcycle : BaseEntity
    {
        [Required]
        public int Year { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicensePlate { get; set; }
    }
}
