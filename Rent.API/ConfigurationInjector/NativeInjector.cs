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
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITokenService, TokenService>();

            //Repositories
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<ILoginRepository, LoginRepository>();
        }
    }
}
