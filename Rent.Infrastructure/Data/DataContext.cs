﻿using Microsoft.EntityFrameworkCore;
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
            {
                options.UseSqlServer(
                    "Server=localhost,1433;Database=Rent;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True"
                );
            }
        }

        public DbSet<Car> Cars => Set<Car>();
        public DbSet<Login> Logins => Set<Login>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Owner> Owners => Set<Owner>();
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<Rental> Rentals => Set<Rental>();
    }
}
