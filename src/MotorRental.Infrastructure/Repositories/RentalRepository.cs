using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class RentalRepository(ApplicationDbContext context) : BaseRepository<Rental>(context), IRentalRepository
    {
    }
}
