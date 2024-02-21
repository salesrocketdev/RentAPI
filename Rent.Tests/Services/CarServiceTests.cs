using Moq;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Services;

namespace Rent.Tests.Services
{
    public class CarServiceTests
    {
        private readonly ICarService _carService;
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly Mock<ICarImageRepository> _carImageRepositoryMock;
        private readonly Mock<IBlobStorageRepository> _blobStorageRepositoryMock;

        public CarServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _carImageRepositoryMock = new Mock<ICarImageRepository>();
            _blobStorageRepositoryMock = new Mock<IBlobStorageRepository>();
            _carService = new CarService(
                _carRepositoryMock.Object,
                _carImageRepositoryMock.Object,
                _blobStorageRepositoryMock.Object
            );
        }

        [Fact]
        public async Task GetAllCars()
        {
            // Given
            int pageNumber = 1;
            int pageSize = 10;
            var expectedCars = new List<Car>
            {
                new Car
                {
                    Id = 1,
                    Brand = "Toyota",
                    Model = "Camry"
                },
                new Car
                {
                    Id = 2,
                    Brand = "Honda",
                    Model = "Civic"
                },
                new Car
                {
                    Id = 3,
                    Brand = "Ford",
                    Model = "Mustang"
                }
            };
            var expectedPagination = new PaginationMeta
            {
                TotalItems = expectedCars.Count,
                TotalPages = 1,
                CurrentPage = 1,
                PageSize = pageSize
            };

            _carRepositoryMock
                .Setup(repo => repo.GetAllCars(pageNumber, pageSize))
                .ReturnsAsync((expectedCars, expectedPagination));

            // When
            var (actualCars, actualPagination) = await _carService.GetAllCars(pageNumber, pageSize);

            // Then
            Assert.Equal(expectedCars, actualCars);
            Assert.Equal(expectedPagination.TotalItems, actualPagination.TotalItems);
            Assert.Equal(expectedPagination.TotalPages, actualPagination.TotalPages);
            Assert.Equal(expectedPagination.CurrentPage, actualPagination.CurrentPage);
            Assert.Equal(expectedPagination.PageSize, actualPagination.PageSize);
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
            int carId = 1;
            var expectedCar = new Car()
            {
                Id = carId,
                Brand = "Ford",
                Model = "Mustang",
                Color = "Branco",
                CreatedAt = DateTime.Now,
                Plate = "ABC123",
                DailyValue = 650,
                Year = 2012,
                Available = true,
                IsActive = true,
                IsDeleted = false,
            };

            _carRepositoryMock.Setup(repo => repo.GetCarById(carId)).ReturnsAsync(expectedCar);

            // When
            var actualCar = await _carService.GetCarById(carId);

            // Then
            Assert.Equal(expectedCar, actualCar);
        }

        [Fact]
        public async Task AddCar()
        {
            // Given
            var newCar = new Car
            {
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

            _carRepositoryMock.Setup(repo => repo.AddCar(newCar)).ReturnsAsync(newCar);

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
            Assert.Equal(newCar.IsActive, addedCar.IsActive);
            Assert.Equal(newCar.IsDeleted, addedCar.IsDeleted);
            Assert.Equal(newCar.CreatedAt, addedCar.CreatedAt);
        }

        [Fact]
        public async void UpdateCar()
        {
            // Given
            var updateCar = new Car()
            {
                Id = 29,
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

            _carRepositoryMock.Setup(repo => repo.UpdateCar(updateCar)).ReturnsAsync(updateCar);

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
            Assert.Equal(updateCar.IsActive, updatedCar.IsActive);
            Assert.Equal(updateCar.IsDeleted, updatedCar.IsDeleted);
            Assert.Equal(updateCar.CreatedAt, updatedCar.CreatedAt);
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
