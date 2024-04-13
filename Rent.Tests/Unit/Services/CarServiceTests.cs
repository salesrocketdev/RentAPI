using AutoMapper;
using Moq;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Services;

namespace Rent.Tests.Unit.Services
{
    public class CarServiceTests
    {
        private readonly ICarService _carService;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<ICarImageRepository> _carImageRepositoryMock;
        private readonly Mock<IBlobStorageRepository> _blobStorageRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CarServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _carImageRepositoryMock = new Mock<ICarImageRepository>();
            _blobStorageRepositoryMock = new Mock<IBlobStorageRepository>();
            _mapperMock = new Mock<IMapper>();

            _carService = new CarService(
                _carRepositoryMock.Object,
                _carImageRepositoryMock.Object,
                _blobStorageRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [Fact]
        public async Task GetAllCars()
        {
            // Given
            int pageNumber = 1;
            int pageSize = 10;

            List<Car> expectedCars =
                new()
                {
                    new()
                    {
                        Id = 1,
                        Brand = "Toyota",
                        Model = "Camry"
                    },
                    new()
                    {
                        Id = 2,
                        Brand = "Honda",
                        Model = "Civic"
                    },
                    new()
                    {
                        Id = 3,
                        Brand = "Ford",
                        Model = "Mustang"
                    }
                };
            PaginationMeta expectedPagination =
                new()
                {
                    TotalItems = expectedCars.Count,
                    TotalPages = 1,
                    CurrentPage = 1,
                    PageSize = pageSize
                };

            _carRepositoryMock
                .Setup(repo => repo.GetAllCars(pageNumber, pageSize))
                .ReturnsAsync(() =>
                {
                    List<Car> cars = _mapperMock.Object.Map<List<Car>>(expectedCars);
                    return (cars, expectedPagination);
                });

            // When
            ResponsePaginateDTO<ResponseCarDTO> responsePaginate = await _carService.GetAllCars(
                pageNumber,
                pageSize
            );

            // Then
            Assert.Equal(
                _mapperMock.Object.Map<List<ResponseCarDTO>>(expectedCars),
                responsePaginate.Data
            );
            Assert.Equal(
                expectedPagination.TotalItems,
                responsePaginate.PaginationMeta?.TotalItems
            );
            Assert.Equal(
                expectedPagination.TotalPages,
                responsePaginate.PaginationMeta?.TotalPages
            );
            Assert.Equal(
                expectedPagination.CurrentPage,
                responsePaginate.PaginationMeta?.CurrentPage
            );
            Assert.Equal(expectedPagination.PageSize, responsePaginate.PaginationMeta?.PageSize);
        }

        [Fact]
        public async Task GetCarById()
        {
            // Given
            int carId = 289;
            var expectedCar = new Car
            {
                Id = carId,
                Brand = "Ford",
                Model = "Mustang",
                Color = "Branco",
                Plate = "ABC123",
                DailyValue = 650,
                Year = 2012,
                Available = true,
            };
            ResponseCarDTO expectedDTO =
                new()
                {
                    Id = expectedCar.Id,
                    Brand = "Ford",
                    Model = "Mustang",
                    Color = "Branco",
                    Plate = "ABC123",
                    DailyValue = 650,
                    Year = 2012,
                    Available = true,
                };

            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).ReturnsAsync(expectedCar);

            _mapperMock
                .Setup(mapper => mapper.Map<ResponseCarDTO>(expectedCar))
                .Returns(expectedDTO);

            // When
            var result = await _carService.GetCarById(carId);

            // Then
            Assert.NotNull(result);
            Assert.Equal(expectedCar.Id, result.Id);
            Assert.Equal(expectedCar.Brand, result.Brand);
            Assert.Equal(expectedCar.Model, result.Model);
            Assert.Equal(expectedCar.Color, result.Color);
            Assert.Equal(expectedCar.Plate, result.Plate);
            Assert.Equal(expectedCar.DailyValue, result.DailyValue);
            Assert.Equal(expectedCar.Year, result.Year);
            Assert.Equal(expectedCar.Available, result.Available);
        }

        [Fact]
        public async Task GetCarById_CarNotFound()
        {
            // Given
            int nonExistentCarId = 999;
            var expectedCar = new Car
            {
                Id = nonExistentCarId,
                Brand = "Ford",
                Model = "Mustang",
                Color = "Branco",
                Plate = "ABC123",
                DailyValue = 650,
                Year = 2012,
                Available = true,
            };

            _carRepositoryMock
                .Setup(repo => repo.GetCarById(nonExistentCarId))
                .ReturnsAsync(expectedCar);

            // Act
            var result = await _carService.GetCarById(nonExistentCarId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AddCar()
        {
            // Given
            var newCarDTO = new CreateCarDTO
            {
                Brand = "Tesla",
                Model = "Model S",
                Year = 2022,
                Color = "Black",
                Plate = "ABC123",
                DailyValue = 100,
                Available = true,
            };

            var expectedCarEntity = new Car()
            {
                Brand = "Tesla",
                Model = "Model S",
                Year = 2022,
                Color = "Black",
                Plate = "ABC123",
                DailyValue = 100,
                Available = true,
            };

            var expectedResponseCarDTO = new ResponseCarDTO()
            {
                Brand = "Tesla",
                Model = "Model S",
                Year = 2022,
                Color = "Black",
                Plate = "ABC123",
                DailyValue = 100,
                Available = true,
            };

            // Configurando o mapper para retornar o entity esperado
            _mapperMock.Setup(m => m.Map<Car>(newCarDTO)).Returns(expectedCarEntity);

            // Configurando o repositório para retornar o entity esperado
            _carRepositoryMock
                .Setup(repo => repo.AddCar(expectedCarEntity))
                .ReturnsAsync(expectedCarEntity);

            // Configurando o mapper para retornar o DTO esperado quando o entity for passado
            _mapperMock
                .Setup(m => m.Map<ResponseCarDTO>(expectedCarEntity))
                .Returns(expectedResponseCarDTO);

            // When
            var addedCar = await _carService.AddCar(newCarDTO);

            // Then
            Assert.NotNull(addedCar);
            Assert.Equal(expectedResponseCarDTO.Brand, addedCar.Brand);
            Assert.Equal(expectedResponseCarDTO.Model, addedCar.Model);
            Assert.Equal(expectedResponseCarDTO.Color, addedCar.Color);
            Assert.Equal(expectedResponseCarDTO.Year, addedCar.Year);
            Assert.Equal(expectedResponseCarDTO.Plate, addedCar.Plate);
            Assert.Equal(expectedResponseCarDTO.Available, addedCar.Available);
            Assert.Equal(expectedResponseCarDTO.DailyValue, addedCar.DailyValue);
        }

        [Fact]
        public async void UpdateCar()
        {
            // Given
            var updateCarDTO = new UpdateCarDTO()
            {
                Id = 29,
                Brand = "Tesla",
                Model = "Model S",
                Color = "Black",
                Year = 2022,
                Plate = "ABC123",
                Available = true,
                DailyValue = 100,
            };

            var expectedCarEntity = new Car()
            {
                Id = 29,
                Brand = "Tesla",
                Model = "Model S",
                Color = "Black",
                Year = 2022,
                Plate = "ABC123",
                Available = true,
                DailyValue = 100,
            };

            var expectedResponseCarDTO = new ResponseCarDTO()
            {
                Id = 29,
                Brand = "Tesla",
                Model = "Model S",
                Color = "Black",
                Year = 2022,
                Plate = "ABC123",
                Available = true,
                DailyValue = 100,
            };

            _mapperMock.Setup(m => m.Map<Car>(updateCarDTO)).Returns(expectedCarEntity);

            _carRepositoryMock
                .Setup(repo => repo.UpdateCar(expectedCarEntity))
                .ReturnsAsync(expectedCarEntity);

            _mapperMock
                .Setup(m => m.Map<ResponseCarDTO>(expectedCarEntity))
                .Returns(expectedResponseCarDTO);

            // When
            var updatedCar = await _carService.UpdateCar(updateCarDTO);

            // Then
            Assert.NotNull(updatedCar);
            Assert.Equal(expectedResponseCarDTO.Id, updatedCar.Id);
            Assert.Equal(expectedResponseCarDTO.Brand, updatedCar.Brand);
            Assert.Equal(expectedResponseCarDTO.Model, updatedCar.Model);
            Assert.Equal(expectedResponseCarDTO.Color, updatedCar.Color);
            Assert.Equal(expectedResponseCarDTO.Year, updatedCar.Year);
            Assert.Equal(expectedResponseCarDTO.Plate, updatedCar.Plate);
            Assert.Equal(expectedResponseCarDTO.Available, updatedCar.Available);
            Assert.Equal(expectedResponseCarDTO.DailyValue, updatedCar.DailyValue);
        }

        [Fact]
        public async Task DeleteCar()
        {
            // Given
            int carId = 1;
            var carToDelete = new Car
            {
                Id = carId,
                Brand = "Tesla",
                Model = "Model S",
                Color = "Black",
                Year = 2022,
                Plate = "ABC123",
                Available = true,
                DailyValue = 100,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };

            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).ReturnsAsync(carToDelete);
            _carRepositoryMock.Setup(repo => repo.DeleteCar(carToDelete.Id)).ReturnsAsync(true);

            // When
            var deletedCar = await _carService.DeleteCar(carId);

            // Then
            Assert.True(deletedCar, "Delete operation should return true");

            _carRepositoryMock.Verify(repo => repo.DeleteCar(carToDelete.Id), Times.Once);
        }
    }
}
