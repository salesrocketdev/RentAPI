using Rent.Core.Models;
using Rent.Domain.Entities;

namespace Rent.Domain.Interfaces
{
    public interface IOwnerRepository
    {
        Task<(List<Owner>, PaginationMeta)> GetAllOwners(int pageNumber, int pageSize);
        Task<Owner> GetOwnerById(int id);
        Task<Owner> AddOwner(Owner owner);
        Task<Owner> UpdateOwner(Owner owner);
        Task DeleteOwner(int id);
    }
}
