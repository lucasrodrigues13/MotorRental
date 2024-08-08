namespace MotorRental.Domain.Dtos
{
    public class UpdateLicensePlateRequest
    {
        public int MotorcycleId { get; set; }
        public string LicensePlate { get; set; }
    }
}
