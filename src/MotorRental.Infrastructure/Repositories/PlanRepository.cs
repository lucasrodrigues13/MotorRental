using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class PlanRepository : BaseRepository<Plan>, IPlanRepository
    {
        public PlanRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
