using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class Rental : BaseEntity
    {
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime? ExpectedEndDate { get; set; }
        [Required]
        public DateTime? EndDate { get; set; }
    }
}
