using Microsoft.AspNetCore.JsonPatch;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Constants;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using Newtonsoft.Json;

namespace MotorRental.Application.Services
{
    public class MotorcycleService : BaseService<Motorcycle>, IMotorcycleService
    {
        private readonly IMotorcyleRepository _motorcyleRepository;
        private readonly IMessagingService _messagingService;
        public MotorcycleService(IMotorcyleRepository motorcyleRepository, IMessagingService messagingService) : base(motorcyleRepository)
        {
            _motorcyleRepository = motorcyleRepository;
            _messagingService = messagingService;
        }

        public IEnumerable<MotorcycleDto> Get(GetMotorcyclesFilterDto getMotorcyclesFilterDto)
        {
            var query = _motorcyleRepository.GetAll();

            if (!string.IsNullOrEmpty(getMotorcyclesFilterDto.LicensePlate))
                query = query.Where(a => a.LicensePlate.ToUpper().Contains(getMotorcyclesFilterDto.LicensePlate.ToUpper()));

            return query.Select(a => new MotorcycleDto
            {
                Id = a.Id,
                LicensePlate = a.LicensePlate,
                Model = a.Model,
                Year = a.Year
            });
        }

        public async Task AddMotorcycle(MotorcycleDto motorcycleDto)
        {
            var motorcycle = await _motorcyleRepository.AddAsync(new Motorcycle
            {
                LicensePlate = motorcycleDto.LicensePlate,
                Model = motorcycleDto.Model,
                Year = motorcycleDto.Year
            });

            _messagingService.SendMessage(RabbitMqConstants.MOTORCYCLE_NOTIFICATION_QUEUE_NAME, JsonConvert.SerializeObject(motorcycle));
        }

        public async Task UpdateLicensePlate(UpdateLicensePlateRequest updateLicensePlateRequest)
        {
            var motorcycle = await GetByIdAsync(updateLicensePlateRequest.MotorcycleId);
            if (motorcycle != null)
            {
                motorcycle.LicensePlate = updateLicensePlateRequest.LicensePlate;
                await _motorcyleRepository.UpdateAsync(motorcycle);
            }
        }
    }
}
