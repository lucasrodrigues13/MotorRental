using Microsoft.AspNetCore.Mvc;
using MotorRental.Application.Interfaces;

namespace MotorRental.WebApi.Controllers
{
    public class DeliverDriverController : Controller
    {
        private readonly IDeliverDriverService _driverService;
        public DeliverDriverController(IDeliverDriverService driverService)
        {
            _driverService = driverService;
        }

        //[HttpPost]
        //public IActionResult UploadLicenseDriverPhoto(IFormFile licenseDriverPhoto)
        //{

        //}
    }
}
