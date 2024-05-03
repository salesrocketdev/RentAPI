using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Repositories;

public interface IBrandRepository
{
    Task<(List<Brand>, PaginationMeta)> GetAllBrands(int pageNumber, int pageSize);
    Task<Brand> GetBrandById(int id);
    Task<Brand> AddBrand(Brand brand);
}
