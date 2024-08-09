using Microsoft.AspNetCore.Mvc;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;

namespace MotorRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorcycleController : ControllerBase
    {
        private readonly IMotorcycleService _service;
        private readonly ILogger<MotorcycleController> _logger;
        public MotorcycleController(IMotorcycleService service, ILogger<MotorcycleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] GetMotorcyclesFilterDto getMotorcyclesFilterDto)
        {
            _logger.LogInformation("Getting motorcycles...");
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MotorcycleDto motorcycleDto)
        {
            await _service.AddAsync(new Motorcycle
            {
                LicensePlate = motorcycleDto.LicensePlate,
                Model = motorcycleDto.Model,
                Year = motorcycleDto.Year
            });

            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateLicensePlate(UpdateLicensePlateRequest updateLicensePlateRequest)
        {
            await _service.UpdateLicensePlate(updateLicensePlateRequest);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteByIdAsync(id);

            return Ok();
        }
    }
}
