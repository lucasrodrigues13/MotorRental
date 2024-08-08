using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class MotorcyleRepository(ApplicationDbContext context) : BaseRepository<Motorcycle>(context), IMotorcyleRepository
    {
    }
}
