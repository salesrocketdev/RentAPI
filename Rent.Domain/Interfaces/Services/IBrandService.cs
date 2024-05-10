using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services;

public interface IBrandService
{
    Task<List<ResponseBrandDTO>> GetAllBrands();
    Task<ResponseBrandDTO> GetBrandById(int id);
    Task<ResponseBrandDTO> AddBrand(CreateBrandDTO createBrandDTO);
}
