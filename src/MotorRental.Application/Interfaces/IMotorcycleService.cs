using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;

namespace MotorRental.Application.Interfaces
{
    public interface IMotorcycleService : IBaseService<Motorcycle>
    {
        IEnumerable<MotorcycleDto> Get(GetMotorcyclesFilterDto getMotorcyclesFilterDto);
        Task UpdateLicensePlate(UpdateLicensePlateRequest updateLicensePlateRequest);
        Task AddMotorcycle(MotorcycleDto motorcycleDto);
    }
}
