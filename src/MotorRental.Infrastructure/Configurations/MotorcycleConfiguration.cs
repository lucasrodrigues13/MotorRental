using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRental.Domain.Entities;

namespace MotorRental.Infrastructure.Configurations
{
    public class MotorcycleConfiguration : BaseEntityConfiguration<Motorcycle>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.Property(u => u.Model).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Year).IsRequired();
            builder.Property(u => u.LicensePlate).IsRequired().HasMaxLength(200);

            builder.HasOne(u => u.Rental).WithOne(u => u.Motorcycle).HasForeignKey<Rental>(u => u.MotorcycleId);
            builder.HasIndex(u => u.LicensePlate).IsUnique();
        }
    }
}
