using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Rent.Domain.Interfaces;
using Rent.Domain.Services;
using Rent.Infrastructure.Repositories;

namespace Rent.API.ConfigurationInjector
{
    public static class NativeInjector
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Services
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<ICustomerService, CustomerService>();

            //Repositories
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();        
        }
    }
}