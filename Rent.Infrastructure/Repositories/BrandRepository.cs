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
        var query = _context.Brands.Where(x => x.IsActive == true && x.IsDeleted == false);

        int totalItems = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        var brands = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        var pagination = new PaginationMeta
        {
            TotalItems = totalItems,
            TotalPages = totalPages,
            CurrentPage = pageNumber,
            PageSize = pageSize
        };

        return (brands, pagination);
    }

    public async Task<Brand> GetBrandById(int id)
    {
        Brand? brand = await _context.Brands.Where(x => x.Id == id).FirstOrDefaultAsync();

        return brand ?? throw new Exception("Brand not found");
    }

    public async Task<Brand> AddBrand(Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();

        return brand;
    }
}
