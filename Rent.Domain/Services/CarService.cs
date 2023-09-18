using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Entities;
using Rent.Core.Models;

namespace Rent.Domain.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<(List<Car>, PaginationMeta)> GetAllCars(int pageNumber, int pageSize)
        {
            return await _carRepository.GetAllCars(pageNumber, pageSize);
        }

        public async Task<Car> GetCarById(int id)
        {
            return await _carRepository.GetCarById(id);
        }

        public async Task<Car> AddCar(Car car)
        {
            return await _carRepository.AddCar(car);
        }

        public async Task<Car> UpdateCar(Car car)
        {
            return await _carRepository.UpdateCar(car);
        }

        public async Task DeleteCar(int id)
        {
            await _carRepository.DeleteCar(id);
        }
    }
}
