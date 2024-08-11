namespace MotorRental.Domain.Entities
{
    public class Plan : BaseEntity
    {
        /// <summary>
        /// Period in days that the plan will last
        /// </summary>
        public int NumberOfDays { get; set; }
        public decimal DailyPrice { get; set; }
        public ICollection<Rental> Rentals { get; set; }
    }
}
