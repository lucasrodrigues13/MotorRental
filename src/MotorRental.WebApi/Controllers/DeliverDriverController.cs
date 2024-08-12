using Microsoft.AspNetCore.Mvc;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Dtos;
using System.Security.Claims;

namespace MotorRental.WebApi.Controllers
{
    //[Authorize(Roles = "Admin,DeliverDriver")]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverDriverController : ApplicationControllerBase
    {
        private readonly IDeliverDriverService _driverService;
        public DeliverDriverController(IDeliverDriverService driverService)
        {
            _driverService = driverService;
        }

        [HttpPost("UploadLicenseDriverPhoto")]
        public async Task<IActionResult> UploadLicenseDriverPhoto(UploadLicenseDriverPhotoDto uploadLicenseDriverPhotoDto)
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            await _driverService.UploadLicenseDriverPhotoAsync(uploadLicenseDriverPhotoDto, userEmail);

            return Ok();
        }
    }
}
