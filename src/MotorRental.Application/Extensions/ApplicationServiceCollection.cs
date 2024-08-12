using MotorRental.Application.Interfaces;
using MotorRental.Application.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IDeliverDriverService, DeliverDriverService>();
            services.AddScoped<IMotorcycleService, MotorcycleService>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IRentalService, RentalService>();

            return services;
        }
    }
}