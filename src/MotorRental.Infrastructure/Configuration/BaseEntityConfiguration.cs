using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorRental.Domain.Entities;

namespace MotorRental.Infrastructure.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Created).IsRequired();
            builder.Property(u => u.LastModified).IsRequired(false);
            builder.Property(u => u.CreatedBy).IsRequired(false);
            builder.Property(u => u.LastModifiedBy).IsRequired(false);
            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);
    }
}