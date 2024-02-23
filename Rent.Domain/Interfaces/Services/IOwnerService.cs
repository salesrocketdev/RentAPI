using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services
{
    public interface IOwnerService
    {
        Task<ResponsePaginateDTO<ResponseOwnerDTO>> GetAllOwners(int pageNumber, int pageSize);
        Task<ResponseOwnerDTO> GetOwnerById(int id);
        Task<ResponseOwnerDTO> AddOwner(CreateOwnerDTO createOwnerDTO);
        Task<ResponseOwnerDTO> UpdateOwner(UpdateOwnerDTO updateOwnerDTO);
        Task<bool> DeleteOwner(int id);
    }
}
