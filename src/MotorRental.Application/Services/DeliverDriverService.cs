using Microsoft.AspNetCore.Http;
using MotorRental.Application.Common;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Constants;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;

namespace MotorRental.Application.Services
{
    public class DeliverDriverService : BaseService<DeliverDriver>, IDeliverDriverService
    {
        private readonly IAwsS3Service _awsS3Service;
        private readonly IDeliverDriverRepository _deliverDriverRepository;
        public DeliverDriverService(IDeliverDriverRepository repository, IAwsS3Service awsS3Service, IDeliverDriverRepository deliverDriverRepository) : base(repository)
        {
            _awsS3Service = awsS3Service;
            _deliverDriverRepository = deliverDriverRepository;
        }
        public async Task<ApiResponse> UploadLicenseDriverPhotoAsync(UploadLicenseDriverPhotoDto uploadLicenseDriverPhotoDto, string userEmail)
        {
            var driver = await _deliverDriverRepository.GetByIdAsync(uploadLicenseDriverPhotoDto.DeliverDriverId);
            var errors = ValidImage(uploadLicenseDriverPhotoDto.LicenseDriverPhoto);
            if (driver == null)
                errors.Add(ErrorMessagesConstants.DELIVER_DRIVER_NOT_REGISTERED);

            if (errors.Count > 0)
                return new ApiResponse(false, ErrorMessagesConstants.BADREQUEST_DEFAULT, null, errors);

            var imagePath = $"licenses/{userEmail}/{uploadLicenseDriverPhotoDto.DeliverDriverId}";
            using (var stream = uploadLicenseDriverPhotoDto.LicenseDriverPhoto.OpenReadStream())
            {
                await _awsS3Service.UploadFileAsync(AwsConstants.S3_BUCKET_NAME, imagePath, stream);
            }
            driver.LicenseDriverImagePath = imagePath;
            await _deliverDriverRepository.UpdateAsync(driver);

            return ApiResponse.Ok();
        }

        private List<string> ValidImage(IFormFile licenseDriverPhoto)
        {
            var errors = new List<string>();

            if (licenseDriverPhoto == null || licenseDriverPhoto.Length == 0)
            {
                errors.Add(ErrorMessagesConstants.PHOTO_EMPTY);
                return errors;
            }

            var fileExtension = Path.GetExtension(licenseDriverPhoto.FileName).ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(licenseDriverPhoto.FileName) || (fileExtension != ".png" && fileExtension != ".bmp"))
                errors.Add(ErrorMessagesConstants.PHOTO_EXTENSION_INVALID);

            return errors;
        }
    }
}
