using Rent.Domain.Entities;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure;

public class BrandSeeder
{
    private readonly DataContext _context;

    public BrandSeeder(DataContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (_context.Brands.Any())
        {
            return;
        }

        var brands = new List<Brand>
        {
            new() { Name = "Audi" },
            new() { Name = "BMW" },
            new() { Name = "Chevrolet" },
            new() { Name = "Ford" },
            new() { Name = "Honda" },
            new() { Name = "Hyundai" },
            new() { Name = "Kia" },
            new() { Name = "Mercedes-Benz" },
            new() { Name = "Nissan" },
            new() { Name = "Toyota" },
            new() { Name = "Volkswagen" },
            new() { Name = "Volvo" },
        };

        _context.Brands.AddRange(brands);
        _context.SaveChanges();
    }
}
