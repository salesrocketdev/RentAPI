using AutoMapper;
using Moq;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Services;

namespace Rent.Tests.Unit.Services;

public class RentalServiceTests
{
    private readonly ICarService _carService;
    private readonly Mock<ICarRepository> _carRepositoryMock;
    private readonly Mock<ICarImageRepository> _carImageRepositoryMock;
    private readonly Mock<IBlobStorageRepository> _blobStorageRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    public RentalServiceTests()
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
    public void TryCreateRentalWithNotAvailableCar()
    {
        // Given

        // When

        // Then
    }
}
