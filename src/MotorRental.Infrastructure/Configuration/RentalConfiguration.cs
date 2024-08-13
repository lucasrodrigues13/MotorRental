using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRental.Domain.Entities;

namespace MotorRental.Infrastructure.Configurations
{
    public class RentalConfiguration : BaseEntityConfiguration<Rental>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Rental> builder)
        {
            builder.Property(u => u.StartDate).IsRequired().HasColumnType("date");
            builder.Property(u => u.EndDate).IsRequired(false).HasColumnType("date");
            builder.Property(u => u.ExpectedEndDate).IsRequired(false).HasColumnType("date");

            builder.HasOne(u => u.Motorcycle).WithOne(u => u.Rental).HasForeignKey<Rental>(u => u.MotorcycleId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(u => u.Plan).WithMany(u => u.Rentals).HasForeignKey(u => u.PlanId);
        }
    }
}