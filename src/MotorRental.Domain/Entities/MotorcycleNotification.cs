using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class MotorcycleNotification
    {
        [Key]
        public int Id { get; set; }
        public int MotorcycleId { get; set; }
        public virtual Motorcycle Motorcycle { get; set; }
    }
}
