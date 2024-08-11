using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorRental.Domain.Entities;
using MotorRental.Infrastructure.Configurations;

namespace MotorRental.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<DeliverDriver> DeliveryDrivers { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DeliverDriverConfiguration());
            builder.ApplyConfiguration(new MotorcycleConfiguration());
            builder.ApplyConfiguration(new PlanConfiguration());
            builder.ApplyConfiguration(new RentalConfiguration());

            builder.Entity<IdentityUserLogin<string>>().HasNoKey();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "admin-role-id",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "driver-role-id",
                    Name = "DeliverDriver",
                    NormalizedName = "DELIVERDRIVER"
                }
            );

            var adminUser = new IdentityUser
            {
                Id = "admin-user-id",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@motorrental.com",
                NormalizedEmail = "ADMIN@MOTORRENTAL.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "AdminPassword123!")
            };

            builder.Entity<IdentityUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = "admin-role-id"
                }
            );
        }
    }
}
