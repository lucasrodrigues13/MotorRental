using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MotorRental.Domain.Entities;

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
                NormalizedEmail = "ADMIN@motorrental.COM",
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
