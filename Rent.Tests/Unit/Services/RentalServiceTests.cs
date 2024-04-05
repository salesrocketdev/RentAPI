using AutoMapper;
using Moq;
using Rent.Core.Models;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Services;

namespace Rent.Tests.Unit.Services;

public class RentalServiceTests
{
    private readonly ICarService _carService;
    private readonly IRentalService _rentalService;
    private readonly Mock<ICarRepository> _carRepositoryMock;
    private readonly Mock<IRentalRepository> _rentalRepositoryMock;
    private readonly Mock<ICarImageRepository> _carImageRepositoryMock;
    private readonly Mock<IBlobStorageRepository> _blobStorageRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public RentalServiceTests()
    {
        _carRepositoryMock = new Mock<ICarRepository>();
        _rentalRepositoryMock = new Mock<IRentalRepository>();
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
    public async Task GetAllRentals()
    {
        // Given
        int pageNumber = 1;
        int pageSize = 10;

        List<Rental> expectedRentals =
            new()
            {
                new()
                {
                    CarId = 294,
                    EmployeeId = 4,
                    CustomerId = 19,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7),
                    SecurityDeposit = 600,
                    Car = new()
                    {
                        Id = 294,
                        Brand = "Ford",
                        Model = "Mustang"
                    },
                    Customer = new()
                    {
                        Name = "Joseph",
                        Document = new() { TaxNumber = "123.456.789.10" }
                    },
                    Employee = new() { Name = "Carlos" }
                }
            };
        PaginationMeta expectedPagination =
            new()
            {
                TotalItems = expectedRentals.Count,
                TotalPages = 1,
                CurrentPage = 1,
                PageSize = pageSize
            };

        _rentalRepositoryMock
            .Setup(repo => repo.GetAllRentals(pageNumber, pageSize))
            .ReturnsAsync(() =>
            {
                List<Rental> rentals = _mapperMock.Object.Map<List<Rental>>(expectedRentals);
                return (rentals, expectedPagination);
            });

        // When
        ResponsePaginateDTO<ResponseRentalDTO> responsePaginate =
            await _rentalService.GetAllRentals(pageNumber, pageSize);

        // Then
        Assert.Equal(
            _mapperMock.Object.Map<List<ResponseRentalDTO>>(expectedRentals),
            responsePaginate.Data
        );
        Assert.Equal(expectedPagination.TotalItems, responsePaginate.PaginationMeta?.TotalItems);
        Assert.Equal(expectedPagination.TotalPages, responsePaginate.PaginationMeta?.TotalPages);
        Assert.Equal(expectedPagination.CurrentPage, responsePaginate.PaginationMeta?.CurrentPage);
        Assert.Equal(expectedPagination.PageSize, responsePaginate.PaginationMeta?.PageSize);
    }

    [Fact]
    public async Task GetCarById()
    {
        // Given
        int rentalId = 289;
        var expectedRental = new Rental
        {
            CarId = 294,
            EmployeeId = 4,
            CustomerId = 19,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(7),
            SecurityDeposit = 600,
            Car = new()
            {
                Id = 294,
                Brand = "Ford",
                Model = "Mustang"
            },
            Customer = new()
            {
                Name = "Joseph",
                Document = new() { TaxNumber = "123.456.789.10" }
            },
            Employee = new() { Name = "Carlos" }
        };
        ResponseRentalDTO expectedDTO =
            new()
            {
                CarId = 294,
                EmployeeId = 4,
                CustomerId = 19,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                SecurityDeposit = 600,
                Car = new()
                {
                    Id = 294,
                    Brand = "Ford",
                    Model = "Mustang"
                },
                Customer = new()
                {
                    Name = "Joseph",
                    Document = new() { TaxNumber = "123.456.789.10" }
                },
                Employee = new() { Name = "Carlos" }
            };

        _rentalRepositoryMock
            .Setup(repo => repo.GetRentalById(rentalId))
            .ReturnsAsync(expectedRental);

        _mapperMock
            .Setup(mapper => mapper.Map<ResponseRentalDTO>(expectedRental))
            .Returns(expectedDTO);

        // When
        var foundRental = await _carService.GetCarById(rentalId);

        // Then
        Assert.NotNull(foundRental);
        Assert.Equal(expectedRental.Id, foundRental.Id);
    }

    [Fact]
    public async Task TryCreateRentalWithNotAvailableCar()
    {
        // Given
        int notAvailableCarId = 294;

        // When

        // Then
    }
}
