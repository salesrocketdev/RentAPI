using Microsoft.Extensions.DependencyInjection;
using Rent.Infrastructure.Seeders;

namespace Rent.Infrastructure.Data
{
    public class DataSeeder
    {
        private readonly IServiceProvider _serviceProvider;

        public DataSeeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Seed()
        {
            using var scope = _serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            // Injete as classes de seeder aqui e chame seus métodos de seed
            var ownerSeeder = services.GetRequiredService<OwnerSeeder>();
            ownerSeeder.Seed();

            var loginSeeder = services.GetRequiredService<LoginSeeder>();
            loginSeeder.Seed();

            var brandSeeder = services.GetRequiredService<BrandSeeder>();
            brandSeeder.Seed();
        }
    }
}
