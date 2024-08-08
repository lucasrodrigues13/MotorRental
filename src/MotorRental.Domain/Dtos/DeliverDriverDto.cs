using MotorRental.Domain.Enums;

namespace MotorRental.Domain.Dtos
{
    public class DeliverDriverDto
    {
        public string FullName { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public long LicenseDriverNumber { get; set; }
        public LicenseDriverTypeEnum LicenseDriverType { get; set; }
        public string? LicenseDriverImagePath { get; set; }
    }
}
