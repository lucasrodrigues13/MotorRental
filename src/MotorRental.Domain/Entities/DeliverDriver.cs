using MotorRental.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MotorRental.Domain.Entities
{
    public class DeliverDriver : BaseEntity
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Cnpj { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public long LicenseDriverNumber { get; set; }
        [Required]
        public LicenseDriverTypeEnum LicenseDriverType { get; set; }
        [Required]
        public string? LicenseDriverImagePath { get; set; }
    }
}
