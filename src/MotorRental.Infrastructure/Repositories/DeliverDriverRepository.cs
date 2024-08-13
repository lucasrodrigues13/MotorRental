using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class DeliverDriverRepository : BaseRepository<DeliverDriver>, IDeliverDriverRepository
    {
        public DeliverDriverRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
