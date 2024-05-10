using AutoMapper;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly IMapper _mapper;

    public BrandService(IBrandRepository brandRepository, IMapper mapper)
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
    }

    public async Task<List<ResponseBrandDTO>> GetAllBrands()
    {
        List<Brand> brands = await _brandRepository.GetAllBrands();

        List<ResponseBrandDTO> responsePaginateDTO = _mapper.Map<List<ResponseBrandDTO>>(brands);

        return responsePaginateDTO;
    }

    public async Task<ResponseBrandDTO> GetBrandById(int id)
    {
        Brand brand = await _brandRepository.GetBrandById(id);

        return _mapper.Map<ResponseBrandDTO>(brand);
    }

    public async Task<ResponseBrandDTO> AddBrand(CreateBrandDTO createBrandDTO)
    {
        Brand mappedBrand = _mapper.Map<Brand>(createBrandDTO);

        var newBrand = await _brandRepository.AddBrand(mappedBrand);

        return _mapper.Map<ResponseBrandDTO>(newBrand);
    }
}
