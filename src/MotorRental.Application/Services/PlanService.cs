using MotorRental.Application.Interfaces;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;

namespace MotorRental.Application.Services
{
    public class PlanService(IBaseRepository<Plan> repository) : BaseService<Plan>(repository), IPlanService
    {
    }
}
