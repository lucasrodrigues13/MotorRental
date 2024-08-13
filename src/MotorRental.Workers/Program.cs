using Microsoft.EntityFrameworkCore;
using MotorRental.Domain.Interfaces;
using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.ExternalServices;
using MotorRental.Infrastructure.Repositories;

namespace MotorRental.Workers
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            CreateHostBuilder(args).Build().Run();
            var host = builder.Build();
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(hostContext.Configuration.GetConnectionString("DefaultConnection")));

                    var motorcycleNotificationQueueSettings = hostContext.Configuration.GetSection("RabbitMQ").Get<MotorcycleNotificationQueueSettings>();

                    services.AddSingleton(motorcycleNotificationQueueSettings);

                    services.AddScoped<IMotorcyleRepository, MotorcyleRepository>();

                    services.AddHostedService<MotorcycleNotificationWorker>();
                });
    }
}