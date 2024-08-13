using MotorRental.Domain.Entities;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;

namespace MotorRental.Infrastructure.Repositories
{
    public class MotorcyleRepository : BaseRepository<Motorcycle>, IMotorcyleRepository
    {
        public MotorcyleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Motorcycle? GetNextAvailableMotorcycle()
        {
            return GetAll().Where(a => a.Rental == null).OrderBy(a => a.LastModified).FirstOrDefault();
        }
    }
}
