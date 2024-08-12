using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotorRental.Application.Common;

namespace MotorRental.WebApi.Controllers
{
    public class ApplicationControllerBase : ControllerBase
    {
        public OkObjectResult Ok()
        {
            return Ok(null);
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            return base.Ok(ApiResponse.Ok(value));
        }

        public BadRequestObjectResult BadRequest(List<string> errors)
        {
            return base.BadRequest(ApiResponse.BadRequest(errors));
        }
    }
}
