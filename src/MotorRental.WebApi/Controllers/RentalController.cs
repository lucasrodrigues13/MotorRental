using Microsoft.AspNetCore.Mvc;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;

namespace MotorRental.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ApplicationControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rentals = _rentalService.GetAll().ToList();
            if (!rentals.Any())
                return NoContent();

            return Ok(rentals);
        }
    }
}
