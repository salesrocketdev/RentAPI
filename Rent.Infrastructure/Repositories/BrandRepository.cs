using Microsoft.EntityFrameworkCore;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Repositories;

public class BrandRepository : BaseRepository<Brand>, IBrandRepository
{
    public BrandRepository(DataContext context)
        : base(context) { }

    public async Task<List<Brand>> GetAllBrands()
    {
        var brands = await (
            from brand in _context.Brands
            where brand.IsActive && !brand.IsDeleted
            join car in _context.Cars on brand.Id equals car.BrandId into carGroup
            from car in carGroup.DefaultIfEmpty()
            group car by new
            {
                brand.Id,
                brand.Name,
                brand.BrandImage,
                brand.IsActive,
                brand.CreatedAt,
                brand.IsDeleted,
                brand.UpdatedAt
            } into g
            select new Brand
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                BrandImage = g.Key.BrandImage,
                Quantity = g.Count(car =>
                    car != null && car.Available && car.IsActive && !car.IsDeleted
                ),
                IsActive = g.Key.IsActive,
                CreatedAt = g.Key.CreatedAt,
                IsDeleted = g.Key.IsDeleted,
                UpdatedAt = g.Key.UpdatedAt
            }
        ).ToListAsync();

        return brands;
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
