using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain;

public class RentalService : IRentalService
{
    private readonly ICarService _carService;
    private readonly IRentalRepository _rentalRepository;
    private readonly IMapper _mapper;

    public RentalService(ICarService carService, IRentalRepository rentalRepository, IMapper mapper)
    {
        _carService = carService;
        _rentalRepository = rentalRepository;
        _mapper = mapper;
    }

    public async Task<ResponsePaginateDTO<ResponseRentalDTO>> GetAllRentals(
        int pageNumber,
        int pageSize
    )
    {
        (List<Rental>, PaginationMeta) employees = await _rentalRepository.GetAllRentals(
            pageNumber,
            pageSize
        );

        var (Data, PaginationMeta) = employees;

        ResponsePaginateDTO<ResponseRentalDTO> responsePaginateDTO =
            new()
            {
                Data = _mapper.Map<List<ResponseRentalDTO>>(Data),
                PaginationMeta = PaginationMeta
            };

        return responsePaginateDTO;
    }

    public async Task<ResponseRentalDTO> GetRentalById(int id)
    {
        Rental rental = await _rentalRepository.GetRentalById(id);

        return _mapper.Map<ResponseRentalDTO>(rental);
    }

    public async Task<ResponseRentalDTO> AddRental(CreateRentalDTO createRentalDTO)
    {
        ResponseCarDTO responseCar =
            await _carService.GetCarById(createRentalDTO.CarId)
            ?? throw new Exception("Car not found.");

        if (responseCar.Available == false)
            throw new Exception("Car already rented.");
        else
            responseCar.Available = false;

        UpdateCarDTO updatedCar = _mapper.Map<UpdateCarDTO>(responseCar);

        await _carService.UpdateCar(updatedCar);

        Rental mappedEmployee = _mapper.Map<Rental>(createRentalDTO);

        var newRental = await _rentalRepository.AddRental(mappedEmployee);

        return _mapper.Map<ResponseRentalDTO>(newRental);
    }

    public async Task<ResponseRentalDTO> UpdateRental(UpdateRentalDTO updateRentalDTO)
    {
        Rental rental = _mapper.Map<Rental>(updateRentalDTO);

        var updatedRental = await _rentalRepository.UpdateRental(rental);

        return _mapper.Map<ResponseRentalDTO>(updatedRental);
    }
}
