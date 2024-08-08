namespace MotorRental.Domain.Dtos
{
    public class RentalDto
    {
        public DateTime StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
