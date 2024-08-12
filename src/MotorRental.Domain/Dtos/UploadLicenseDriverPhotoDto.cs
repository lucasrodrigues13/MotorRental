using Microsoft.AspNetCore.Http;

namespace MotorRental.Domain.Dtos
{
    public class UploadLicenseDriverPhotoDto
    {
        public int DeliverDriverId { get; set; }
        public IFormFile LicenseDriverPhoto { get; set; }
    }
}
