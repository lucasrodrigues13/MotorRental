using Microsoft.AspNetCore.JsonPatch;
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
        public MotorcycleController(IMotorcycleService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] GetMotorcyclesFilterDto getMotorcyclesFilterDto)
        {
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
    }
}
