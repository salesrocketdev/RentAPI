using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services
{
    public interface IRentalService
    {
        Task<ResponsePaginateDTO<ResponseRentalDTO>> GetAllRentals(int pageNumber, int pageSize);
        Task<ResponseRentalDTO> GetRentalById(int id);
        Task<ResponseRentalDTO> AddRental(CreateRentalDTO createRentalDTO);
        Task<ResponseRentalDTO> UpdateRental(UpdateRentalDTO updateRentalDTO);
    }
}
