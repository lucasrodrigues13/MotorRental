using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRental.Domain.Entities;

namespace MotorRental.Infrastructure.Configuration
{
    public class MotorcycleNotificationConfiguration : IEntityTypeConfiguration<MotorcycleNotification>
    {
        public void Configure(EntityTypeBuilder<MotorcycleNotification> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasOne(u => u.Motorcycle).WithOne(u => u.MotorcycleNotification)
                .HasForeignKey<MotorcycleNotification>(u => u.MotorcycleId);
        }
    }
}
