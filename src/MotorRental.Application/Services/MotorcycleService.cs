using Microsoft.AspNetCore.JsonPatch;
using MotorRental.Application.Interfaces;
using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;

namespace MotorRental.Application.Services
{
    public class MotorcycleService : BaseService<Motorcycle>, IMotorcycleService
    {
        private readonly IMotorcyleRepository _motorcyleRepository;
        public MotorcycleService(IBaseRepository<Motorcycle> repository, IMotorcyleRepository motorcyleRepository) : base(repository)
        {
            _motorcyleRepository = motorcyleRepository;
        }

        public async Task<IEnumerable<MotorcycleDto>> GetAllWithFilter(GetMotorcyclesFilterDto getMotorcyclesFilterDto)
        {
            var query = _motorcyleRepository.GetAll();

            if (!string.IsNullOrEmpty(getMotorcyclesFilterDto.LicensePlate))
                query.Where(a => a.LicensePlate.ToUpper().Equals(getMotorcyclesFilterDto.LicensePlate.ToUpper()));

            return query.Select(a => new MotorcycleDto
            {
                Id = a.Id,
                LicensePlate = a.LicensePlate,
                Model = a.Model,
                Year = a.Year
            });
        }

        public async Task UpdateLicensePlate(UpdateLicensePlateRequest updateLicensePlateRequest)
        {
            var motorcycle = await GetByIdAsync(updateLicensePlateRequest.MotorcycleId);
            if (motorcycle != null)
            {
                motorcycle.LicensePlate = updateLicensePlateRequest.LicensePlate;
                await _motorcyleRepository.UpdateAsync(motorcycle);
                await _motorcyleRepository.SaveAsync();
            }
        }
    }
}
