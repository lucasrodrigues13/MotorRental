using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRental.Domain.Entities;

namespace MotorRental.Infrastructure.Configurations
{
    public class PlanConfiguration : BaseEntityConfiguration<Plan>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Plan> builder)
        {
            builder.Property(u => u.NumberOfDays).IsRequired().HasMaxLength(200);
            builder.Property(u => u.DailyPrice).IsRequired();

            builder.HasMany(u => u.Rentals).WithOne(u => u.Plan).HasForeignKey(u => u.PlanId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}