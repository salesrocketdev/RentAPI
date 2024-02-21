using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces.Services
{
    public interface ICarService
    {
        Task<(List<Car>, PaginationMeta)> GetAllCars(int pageNumber, int pageSize);
        Task<Car> GetCarById(int id);
        Task<Car> AddCar(Car car);
        Task<Car> UpdateCar(Car car);
        Task UploadImage(int carId, MemoryStream memoryStream);
        Task DeleteCar(int id);
    }
}
