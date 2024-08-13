using Amazon.S3;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.ExternalServices;
using MotorRental.Infrastructure.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            var motorcycleNotificationQueueSettings = configuration.GetSection("RabbitMQ").Get<MotorcycleNotificationQueueSettings>();
            services.AddSingleton(motorcycleNotificationQueueSettings);

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireUppercase = true;
                options.User.RequireUniqueEmail = true;

            }).AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager<SignInManager<IdentityUser>>()
            .AddDefaultTokenProviders();

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IDeliverDriverRepository, DeliverDriverRepository>();
            services.AddScoped<IMotorcyleRepository, MotorcyleRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>(); 

            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonS3>();
            services.AddSingleton<IAwsS3Service, AwsS3Service>();
            services.AddTransient<IMessagingService, RabbitMqService>();

            return services;
        }
    }
}