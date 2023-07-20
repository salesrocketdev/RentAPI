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
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            //Repositories
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
