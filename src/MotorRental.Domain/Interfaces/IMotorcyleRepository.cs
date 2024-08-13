using MotorRental.Domain.Entities;

namespace MotorRental.Domain.Interfaces
{
    public interface IMotorcyleRepository : IBaseRepository<Motorcycle>
    {
        Motorcycle? GetNextAvailableMotorcycle();
    }
}
