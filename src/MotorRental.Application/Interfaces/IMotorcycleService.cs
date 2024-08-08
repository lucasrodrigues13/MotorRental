using MotorRental.Domain.Dtos;
using MotorRental.Domain.Entities;

namespace MotorRental.Application.Interfaces
{
    public interface IMotorcycleService : IBaseService<Motorcycle>
    {
        Task<IEnumerable<MotorcycleDto>> GetAllWithFilter(GetMotorcyclesFilterDto getMotorcyclesFilterDto);
        Task UpdateLicensePlate(UpdateLicensePlateRequest updateLicensePlateRequest);
    }
}
