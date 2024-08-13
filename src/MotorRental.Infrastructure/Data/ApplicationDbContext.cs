using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorRental.Domain.Constants;
using MotorRental.Domain.Entities;
using MotorRental.Infrastructure.Configuration;
using MotorRental.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore.Proxies;

namespace MotorRental.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<DeliverDriver> DeliveryDrivers { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new DeliverDriverConfiguration());
            builder.ApplyConfiguration(new MotorcycleConfiguration());
            builder.ApplyConfiguration(new PlanConfiguration());
            builder.ApplyConfiguration(new RentalConfiguration());
            builder.ApplyConfiguration(new MotorcycleNotificationConfiguration());

            var roleAdmin = new IdentityRole
            {
                Name = MotorRentalIdentityConstants.ADMIN_ROLE_NAME,
                NormalizedName = MotorRentalIdentityConstants.ADMIN_ROLE_NORMALIZED_NAME
            };

            var roleDeliverDriver = new IdentityRole
            {
                Name = MotorRentalIdentityConstants.DELIVER_DRIVER_ROLE_NAME,
                NormalizedName = MotorRentalIdentityConstants.DELIVER_DRIVER_ROLE_NORMALIZED_NAME
            };

            builder.Entity<IdentityRole>().HasData(roleAdmin, roleDeliverDriver);

            var adminUser = new IdentityUser
            {
                UserName = MotorRentalIdentityConstants.ADMIN_USERNAME_DEFAULT,
                NormalizedUserName = MotorRentalIdentityConstants.ADMIN_NORMALIZED_USERNAME_DEFAULT,
                Email = MotorRentalIdentityConstants.ADMIN_USER_EMAIL_DEFAULT,
                NormalizedEmail = MotorRentalIdentityConstants.ADMIN_NORMALIZED_EMAIL_DEFAULT,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, MotorRentalIdentityConstants.ADMIN_USER_PASSWORD_DEFAULT)
            };

            builder.Entity<IdentityUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminUser.Id,
                    RoleId = roleAdmin.Id
                }
            );

            builder.Entity<Plan>().HasData(
                new Plan
                {
                    Id = 1,
                    NumberOfDays = 7,
                    DailyPrice = 30,
                    CreatedBy = adminUser.Id
                },
                new Plan
                {
                    Id = 2,
                    NumberOfDays = 15,
                    DailyPrice = 28,
                    CreatedBy = adminUser.Id
                },
                new Plan
                {
                    Id = 3,
                    NumberOfDays = 30,
                    DailyPrice = 22,
                    CreatedBy = adminUser.Id
                },
                new Plan
                {
                    Id = 4,
                    NumberOfDays = 45,
                    DailyPrice = 20,
                    CreatedBy = adminUser.Id
                },
                new Plan
                {
                    Id = 5,
                    NumberOfDays = 50,
                    DailyPrice = 18,
                    CreatedBy = adminUser.Id
                }
            );
        }
    }
}
