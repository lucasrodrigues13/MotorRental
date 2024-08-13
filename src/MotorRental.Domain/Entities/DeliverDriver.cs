using MotorRental.Domain.Enums;

namespace MotorRental.Domain.Entities
{
    public class DeliverDriver : BaseEntity
    {
        public string FullName { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public long LicenseDriverNumber { get; set; }
        public LicenseDriverTypeEnum LicenseDriverType { get; set; }
        public string? LicenseDriverImagePath { get; set; }
        public string IdentityUserId { get; set; }
        public string Email { get; set; }
        public virtual Rental Rental { get; set; }
    }
}
