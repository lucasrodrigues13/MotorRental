namespace MotorRental.Domain.Dtos
{
    public class RentAMotorcycleDto
    {
        public int PlanId { get; set; }
        public int MotorcycleId { get; set; }
        public int DeliverDriverId { get; set; }
        public DateTime StartDate { get; } = DateTime.Now.AddDays(1);
    }
}
