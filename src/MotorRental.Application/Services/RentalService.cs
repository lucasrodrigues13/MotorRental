using MotorRental.Application.Interfaces;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;

namespace MotorRental.Application.Services
{
    public class RentalService(IBaseRepository<Rental> repository) : BaseService<Rental>(repository), IRentalService
    {
    }
}
