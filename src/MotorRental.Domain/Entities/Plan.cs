using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class Plan
    {
        /// <summary>
        /// Period in days that the plan will last
        /// </summary>
        [Required]
        public int NumberOfDays { get; set; }
        [Required]
        public decimal DailyPrice { get; set; }
    }
}
