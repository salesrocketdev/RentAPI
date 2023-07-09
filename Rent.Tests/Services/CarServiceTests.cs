using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using Rent.Core.Models;
using Xunit;
using System.Runtime.ConstrainedExecution;
using Rent.Domain.Services;

namespace Rent.Tests.Services
{
    public class CarServiceTests
    {
        private readonly Mock<ICarRepository> _carRepositoryMock;
        private readonly ICarService _carService;

        public CarServiceTests()
        {
            _carRepositoryMock = new Mock<ICarRepository>();
            _carService = new CarService(_carRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllCars()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            var expectedCars = new List<Car> 
            { 
                new Car { Id = 1, Brand = "Toyota", Model = "Camry" },
                new Car { Id = 2, Brand = "Honda", Model = "Civic" },
                new Car { Id = 3, Brand = "Ford", Model = "Mustang" } 
            };
            var expectedPagination = new PaginationMeta
            {
                TotalItems = expectedCars.Count,
                TotalPages = 1,
                CurrentPage = 1,
                PageSize = pageSize
            };

            _carRepositoryMock.Setup(repo => repo.GetAllCars(pageNumber, pageSize))
                .ReturnsAsync((expectedCars, expectedPagination));

            // Act
            var (actualCars, actualPagination) = await _carService.GetAllCars(pageNumber, pageSize);

            // Assert
            Assert.Equal(expectedCars, actualCars);
            Assert.Equal(expectedPagination.TotalItems, actualPagination.TotalItems);
            Assert.Equal(expectedPagination.TotalPages, actualPagination.TotalPages);
            Assert.Equal(expectedPagination.CurrentPage, actualPagination.CurrentPage);
            Assert.Equal(expectedPagination.PageSize, actualPagination.PageSize);
        }

        [Fact]
        public async Task AddCar()
        {
            var car = new Car
            {
                Brand = "Tesla",
                Model = "Model S",
                Color = "Black",
                Year = 2022,
                Plate = "ABC123",
                Available = true,
                Value = 350,
                IsActive = true,
                IsDeleted = false,
                CreatedAt = DateTime.Now
            };

            _carRepositoryMock.Setup(repo => repo.AddCar(car))
                             .ReturnsAsync(car);

            // Act
            var addedCar = await _carService.AddCar(car);

            // Assert
            Assert.NotNull(addedCar);
            Assert.Equal(car.Brand, addedCar.Brand);
            Assert.Equal(car.Model, addedCar.Model);
            Assert.Equal(car.Color, addedCar.Color);
            Assert.Equal(car.Year, addedCar.Year);
            Assert.Equal(car.Plate, addedCar.Plate);
            Assert.Equal(car.Available, addedCar.Available);
            Assert.Equal(car.Value, addedCar.Value);
            Assert.Equal(car.IsActive, addedCar.IsActive);
            Assert.Equal(car.IsDeleted, addedCar.IsDeleted);
            Assert.Equal(car.CreatedAt, addedCar.CreatedAt);
        }

    }
}
