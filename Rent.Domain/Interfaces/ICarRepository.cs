using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces
{
    public interface ICarRepository 
    {
        Task<(List<Car>, PaginationMeta)> GetAllCars(int pageNumber, int pageSize);
        Task<Car> GetCarById(int id);
        Task<Car> AddCar(Car car);
        Task<Car> UpdateCar(Car car);
        Task DeleteCar(int id);
    }
}
