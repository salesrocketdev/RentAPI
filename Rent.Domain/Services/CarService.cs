using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarImageRepository _carImageRepository;
        private readonly IBlobStorageRepository _blobStorageRepository;

        public CarService(
            ICarRepository carRepository,
            ICarImageRepository carImageRepository,
            IBlobStorageRepository blobStorageRepository
        )
        {
            _carRepository = carRepository;
            _carImageRepository = carImageRepository;
            _blobStorageRepository = blobStorageRepository;
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

        public async Task UploadImage(int carId, MemoryStream memoryStream)
        {
            string blobName = carId + "/" + Guid.NewGuid().ToString() + ".jpeg";

            string uploadImage = await _blobStorageRepository.UploadBlobAsync(
                memoryStream,
                blobName
            );

            if (uploadImage != string.Empty)
            {
                List<CarImage> carImages = await _carImageRepository.GetImageByCarId(carId);

                CarImage newCarImage =
                    new()
                    {
                        CarId = carId,
                        Link = uploadImage,
                        IsPrimary = carImages.Count == 0
                    };

                await _carImageRepository.AddCarImage(newCarImage);
            }
        }

        public async Task DeleteCar(int id)
        {
            await _carRepository.DeleteCar(id);
        }
    }
}
