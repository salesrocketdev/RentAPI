using Microsoft.EntityFrameworkCore;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories;

public class BrandRepository : BaseRepository<Brand>, IBrandRepository
{
    public BrandRepository(DataContext context)
        : base(context) { }

    public async Task<(List<Brand>, PaginationMeta)> GetAllBrands(int pageNumber, int pageSize)
    {
        var brands = await _context
            .Brands.Where(x => x.IsActive && !x.IsDeleted)
            .GroupJoin(
                _context.Cars.Where(x => x.Available && x.IsActive && !x.IsDeleted),
                brand => brand.Id,
                car => car.BrandId,
                (brand, cars) => new { Brand = brand, CarCount = cars.Count() }
            )
            .Select(x => new Brand
            {
                Id = x.Brand.Id,
                Name = x.Brand.Name,
                Quantity = x.CarCount,
                IsActive = x.Brand.IsActive,
                CreatedAt = x.Brand.CreatedAt,
                IsDeleted = x.Brand.IsDeleted,
                UpdatedAt = x.Brand.UpdatedAt,
            })
            .ToListAsync();

        var pagination = new PaginationMeta { };

        return (brands, pagination);
    }

    public async Task<Brand> GetBrandById(int id)
    {
        Brand? brand = await _context
            .Brands.Where(x => x.Id == id && x.IsActive == true && x.IsDeleted == false)
            .FirstOrDefaultAsync();

        return brand ?? throw new Exception("Brand not found");
    }

    public async Task<Brand> AddBrand(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        return brand;
    }
}
