using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Repositories;

public interface ICarImageRepository
{
    Task<List<CarImage>> GetImageByCarId(int carId);
    Task<CarImage> AddCarImage(CarImage carImage);
}
