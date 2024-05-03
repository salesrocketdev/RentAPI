using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;

namespace Rent.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseSqlServer(
                    "workstation id=RentDatabase.mssql.somee.com;packet size=4096;user id=salesrocketdev_SQLLogin_1;pwd=iwb7i1abem;data source=RentDatabase.mssql.somee.com;persist security info=False;initial catalog=RentDatabase;TrustServerCertificate=True"
                );
        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<CarImage> CarImages => Set<CarImage>();
        public DbSet<Brand> Brands => Set<Brand>();
        public DbSet<Login> Logins => Set<Login>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<RevokedToken> RevokedTokens => Set<RevokedToken>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Rental> Rentals => Set<Rental>();
    }
}
