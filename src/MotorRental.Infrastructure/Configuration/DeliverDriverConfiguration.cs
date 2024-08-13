using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRental.Domain.Entities;
using MotorRental.Domain.Enums;

namespace MotorRental.Infrastructure.Configurations
{
    public class DeliverDriverConfiguration : BaseEntityConfiguration<DeliverDriver>
    {
        public override void ConfigureEntity(EntityTypeBuilder<DeliverDriver> builder)
        {
            builder.Property(u => u.LicenseDriverNumber).IsRequired();
            builder.Property(u => u.BirthDate).IsRequired().HasColumnType("date");
            builder.Property(u => u.Cnpj).IsRequired().HasMaxLength(14);
            builder.Property(u => u.FullName).IsRequired().HasMaxLength(100);
            builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.IdentityUserId).IsRequired();
            builder.Property(u => u.LicenseDriverImagePath).IsRequired(false);
            builder.Property(u => u.LicenseDriverType).IsRequired()
               .HasConversion(
                   u => u.ToString(),
                   u => (LicenseDriverTypeEnum)Enum.Parse(typeof(LicenseDriverTypeEnum), u))
               .HasMaxLength(2);

            builder.HasOne(u => u.Rental).WithOne(u => u.DeliverDriver).HasForeignKey<Rental>(u => u.MotorcycleId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(u => u.Cnpj).IsUnique();
            builder.HasIndex(u => u.LicenseDriverNumber).IsUnique();
        }
    }
}