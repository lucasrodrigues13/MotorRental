using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MotorRental.Application.Common;

namespace MotorRental.WebApi.Controllers
{
    [ApiController]
    public class ApplicationControllerBase : ControllerBase
    {
        protected OkObjectResult Ok()
        {
            return Ok(null);
        }

        protected OkObjectResult Ok([ActionResultObjectValue] object? value)
        {
            return base.Ok(ApiResponse.Ok(value));
        }
        protected OkObjectResult Ok(ApiResponse value)
        {
            return base.Ok(value);
        }

        protected BadRequestObjectResult BadRequest(ApiResponse apiResponse)
        {
            return base.BadRequest(apiResponse);
        }

        protected BadRequestObjectResult BadRequest(List<string> errors)
        {
            return base.BadRequest(ApiResponse.BadRequest(errors));
        }
    }
}
