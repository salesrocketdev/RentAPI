using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _brandRepository;
    private readonly ICarRepository _carRepository;
    private readonly IMapper _mapper;

    public BrandService(
        IBrandRepository brandRepository,
        ICarRepository carRepository,
        IMapper mapper
    )
    {
        _brandRepository = brandRepository;
        _carRepository = carRepository;
        _mapper = mapper;
    }

    public async Task<ResponsePaginateDTO<ResponseBrandDTO>> GetAllBrands(
        int pageNumber,
        int pageSize
    )
    {
        (List<Brand>, PaginationMeta) brands = await _brandRepository.GetAllBrands(
            pageNumber,
            pageSize
        );

        var (Data, PaginationMeta) = brands;

        ResponsePaginateDTO<ResponseBrandDTO> responsePaginateDTO =
            new()
            {
                Data = _mapper.Map<List<ResponseBrandDTO>>(Data),
                PaginationMeta = PaginationMeta
            };

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
