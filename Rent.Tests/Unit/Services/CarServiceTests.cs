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

        // Given
        // Aqui você configuraria o estado inicial necessário para o teste,
        // como criar instâncias de objetos ou configurar o ambiente de teste.

        // When
        // Aqui você chamaria o método que está sendo testado,
        // fornecendo os dados de entrada necessários, como o ID do carro.

        // Then
        // Aqui você faria as asserções para verificar se o método
        // GetCarById se comportou conforme o esperado, retornando o carro correto.

        [Fact]
        public async Task GetCarById()
        {
            // Given
            int carId = 289;
            ResponseCarDTO expectedCar =
                new()
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

            _carRepositoryMock
                .Setup(repo => repo.GetCarById(carId))
                .Returns(() =>
                {
                    var car = _mapperMock.Object.Map<Car>(expectedCar);
                    return Task.FromResult(car);
                });

            // When
            var actualCar = await _carService.GetCarById(carId);

            // Then
            Assert.NotNull(expectedCar);

            Assert.Equal(expectedCar.Id, actualCar.Id);
        }

        [Fact]
        public async Task AddCar()
        {
            // Given
            var newCar = new CreateCarDTO
            {
                Brand = "Tesla",
                Model = "Model S",
                Color = "Black",
                Year = 2022,
                Plate = "ABC123",
                Available = true,
                DailyValue = 100,
            };

            Car mappedNewCar = _mapperMock.Object.Map<Car>(newCar);

            _carRepositoryMock
                .Setup(repo => repo.AddCar(mappedNewCar))
                .Returns(() =>
                {
                    _mapperMock.Object.Map<ResponseCarDTO>(newCar);
                });

            // When
            var addedCar = await _carService.AddCar(newCar);

            // Then
            Assert.NotNull(addedCar);
            Assert.Equal(newCar.Brand, addedCar.Brand);
            Assert.Equal(newCar.Model, addedCar.Model);
            Assert.Equal(newCar.Color, addedCar.Color);
            Assert.Equal(newCar.Year, addedCar.Year);
            Assert.Equal(newCar.Plate, addedCar.Plate);
            Assert.Equal(newCar.Available, addedCar.Available);
            Assert.Equal(newCar.DailyValue, addedCar.DailyValue);
        }

        [Fact]
        public async void UpdateCar()
        {
            // Given
            var updateCar = new UpdateCarDTO()
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

            Car mappedUpdateCar = _mapperMock.Object.Map<Car>(updateCar);

            _carRepositoryMock
                .Setup(repo => repo.UpdateCar(mappedUpdateCar))
                .Returns(() =>
                {
                    _mapperMock.Object.Map<ResponseCarDTO>(updateCar);
                });

            // When
            var updatedCar = await _carService.UpdateCar(updateCar);

            // Then
            Assert.NotNull(updatedCar);
            Assert.Equal(updateCar.Id, updatedCar.Id);
            Assert.Equal(updateCar.Brand, updatedCar.Brand);
            Assert.Equal(updateCar.Model, updatedCar.Model);
            Assert.Equal(updateCar.Color, updatedCar.Color);
            Assert.Equal(updateCar.Year, updatedCar.Year);
            Assert.Equal(updateCar.Plate, updatedCar.Plate);
            Assert.Equal(updateCar.Available, updatedCar.Available);
            Assert.Equal(updateCar.DailyValue, updatedCar.DailyValue);
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
            var result = await _carService.DeleteCar(carId);

            // Then
            Assert.True(result, "Delete operation should return true");

            // Verify that the delete method was called with the correct car
            _carRepositoryMock.Verify(repo => repo.DeleteCar(carToDelete.Id), Times.Once);
        }
    }
}
