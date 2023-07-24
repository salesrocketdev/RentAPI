using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Entities;
using Rent.Core.Models;

namespace Rent.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;

        public OwnerService(IOwnerRepository ownerRepository)
        {
            _ownerRepository = ownerRepository;
        }

        public async Task<(List<Owner>, PaginationMeta)> GetAllOwners(int pageNumber, int pageSize)
        {
            return await _ownerRepository.GetAllOwners(pageNumber, pageSize);
        }

        public async Task<Owner> GetOwnerById(int id)
        {
            return await _ownerRepository.GetOwnerById(id);
        }

        public async Task<Owner> AddOwner(Owner owner)
        {
            return await _ownerRepository.AddOwner(owner);
        }

        public async Task<Owner> UpdateOwner(Owner owner)
        {
            return await _ownerRepository.AddOwner(owner);
        }

        public async Task DeleteOwner(int id)
        {
            await _ownerRepository.GetOwnerById(id);
        }
    }
}
