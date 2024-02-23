using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services
{
    public interface ICarService
    {
        Task<ResponsePaginateDTO<ResponseCarDTO>> GetAllCars(int pageNumber, int pageSize);
        Task<ResponseCarDTO> GetCarById(int id);
        Task<ResponseCarDTO> AddCar(CreateCarDTO createCarDTO);
        Task<ResponseCarDTO> UpdateCar(UpdateCarDTO updateCarDTO);
        Task UploadImage(int carId, MemoryStream memoryStream);
        Task<bool> DeleteCar(int id);
    }
}
