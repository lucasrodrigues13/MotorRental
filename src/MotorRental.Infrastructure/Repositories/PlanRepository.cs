using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class PlanRepository(ApplicationDbContext context) : BaseRepository<Plan>(context), IPlanRepository
    {
    }
}
