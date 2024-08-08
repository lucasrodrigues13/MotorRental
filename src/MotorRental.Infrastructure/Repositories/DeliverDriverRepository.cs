using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class DeliverDriverRepository(ApplicationDbContext context) : BaseRepository<DeliverDriver>(context), IDeliverDriverRepository
    {
    }
}
