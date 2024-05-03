using Rent.Domain;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Services;
using Rent.Infrastructure;
using Rent.Infrastructure.Data;
using Rent.Infrastructure.Repositories;
using Rent.Infrastructure.Seeders;

namespace Rent.API.ConfigurationInjector
{
    public static class NativeInjector
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IBlobStorageRepository>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("AzureBlobStorage");

                return new BlobStorageRepository(connectionString);
            });

            //Data Seeders
            services.AddTransient<DataSeeder>();
            services.AddTransient<LoginSeeder>();
            services.AddTransient<OwnerSeeder>();
            services.AddTransient<BrandSeeder>();

            //Services
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IOwnerService, OwnerService>();
            services.AddTransient<IRentalService, RentalService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ISecurityService, SecurityService>();

            //Repositories
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<ICarImageRepository, CarImageRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
        }
    }
}
