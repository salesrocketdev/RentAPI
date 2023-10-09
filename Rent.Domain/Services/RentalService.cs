using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain;

public class RentalService : IRentalService
{
    private readonly ICarService _carService;
    private readonly IRentalRepository _rentalRepository;

    public RentalService(ICarService carService, IRentalRepository rentalRepository)
    {
        _carService = carService;
        _rentalRepository = rentalRepository;
    }

    public async Task<(List<Rental>, PaginationMeta)> GetAllRentals(int pageNumber, int pageSize)
    {
        return await _rentalRepository.GetAllRentals(pageNumber, pageSize);
    }

    public async Task<Rental> GetRentalById(int id)
    {
        return await _rentalRepository.GetRentalById(id);
    }

    public async Task<Rental> AddRental(Rental rental)
    {
        var car = await _carService.GetCarById(rental.CarId);

        if (car.Available == false)
            throw new Exception("Car already rented.");
        else car.Available = false;

        await _carService.UpdateCar(car);

        return await _rentalRepository.AddRental(rental);
    }

    public async Task<Rental> UpdateRental(Rental rental)
    {
        return await _rentalRepository.UpdateRental(rental);
    }
}
