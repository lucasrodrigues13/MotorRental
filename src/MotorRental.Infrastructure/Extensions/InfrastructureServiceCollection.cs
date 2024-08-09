using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDeliverDriverRepository, DeliverDriverRepository>();
            services.AddScoped<IMotorcyleRepository, MotorcyleRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            return services;
        }
    }
}