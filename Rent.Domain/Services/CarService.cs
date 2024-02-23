using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
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
        private readonly IMapper _mapper;

        public CarService(
            ICarRepository carRepository,
            ICarImageRepository carImageRepository,
            IBlobStorageRepository blobStorageRepository,
            IMapper mapper
        )
        {
            _carRepository = carRepository;
            _carImageRepository = carImageRepository;
            _blobStorageRepository = blobStorageRepository;
            _mapper = mapper;
        }

        public async Task<ResponsePaginateDTO<ResponseCarDTO>> GetAllCars(
            int pageNumber,
            int pageSize
        )
        {
            (List<Car>, PaginationMeta) cars = await _carRepository.GetAllCars(
                pageNumber,
                pageSize
            );

            var (Data, PaginationMeta) = cars;

            ResponsePaginateDTO<ResponseCarDTO> responsePaginateDTO =
                new()
                {
                    Data = _mapper.Map<List<ResponseCarDTO>>(Data),
                    PaginationMeta = PaginationMeta
                };

            return responsePaginateDTO;
        }

        public async Task<ResponseCarDTO> GetCarById(int id)
        {
            Car car = await _carRepository.GetCarById(id);

            return _mapper.Map<ResponseCarDTO>(car);
        }

        public async Task<ResponseCarDTO> AddCar(CreateCarDTO createCarDTO)
        {
            Car mappedCar = _mapper.Map<Car>(createCarDTO);

            var newCar = await _carRepository.AddCar(mappedCar);

            return _mapper.Map<ResponseCarDTO>(newCar);
        }

        public async Task<ResponseCarDTO> UpdateCar(UpdateCarDTO updateCarDTO)
        {
            Car mappedCar = _mapper.Map<Car>(updateCarDTO);

            var updatedCar = await _carRepository.UpdateCar(mappedCar);

            return _mapper.Map<ResponseCarDTO>(updatedCar);
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

        public async Task<bool> DeleteCar(int id)
        {
            return await _carRepository.DeleteCar(id);
        }
    }
}
