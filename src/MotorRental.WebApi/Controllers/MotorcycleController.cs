using Microsoft.AspNetCore.Mvc;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Dtos;

namespace MotorRental.WebApi.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleController : ApplicationControllerBase
    {
        private readonly IMotorcycleService _motorcycleService;
        public MotorcycleController(IMotorcycleService service, ILogger<MotorcycleController> logger)
        {
            _motorcycleService = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] GetMotorcyclesFilterDto getMotorcyclesFilterDto)
        {
            var motorcycles = _motorcycleService.Get(getMotorcyclesFilterDto);

            if (!motorcycles.Any())
                return NoContent();

            return Ok(motorcycles);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotorcycleDto motorcycleDto)
        {
            await _motorcycleService.AddMotorcycle(motorcycleDto);

            return Ok();
        }

        [HttpPatch("UpdateLicensePlate")]
        public async Task<IActionResult> UpdateLicensePlate(UpdateLicensePlateRequest updateLicensePlateRequest)
        {
            await _motorcycleService.UpdateLicensePlate(updateLicensePlateRequest);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _motorcycleService.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
