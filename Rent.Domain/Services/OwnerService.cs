using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

namespace Rent.Domain.Services
{
    public class OwnerService : IOwnerService
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public OwnerService(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<ResponsePaginateDTO<ResponseOwnerDTO>> GetAllOwners(
            int pageNumber,
            int pageSize
        )
        {
            (List<Owner>, PaginationMeta) customers = await _ownerRepository.GetAllOwners(
                pageNumber,
                pageSize
            );

            var (Data, PaginationMeta) = customers;

            ResponsePaginateDTO<ResponseOwnerDTO> responsePaginateDTO =
                new()
                {
                    Data = _mapper.Map<List<ResponseOwnerDTO>>(Data),
                    PaginationMeta = PaginationMeta
                };

            return responsePaginateDTO;
        }

        public async Task<ResponseOwnerDTO> GetOwnerById(int id)
        {
            Owner owner = await _ownerRepository.GetOwnerById(id);

            return _mapper.Map<ResponseOwnerDTO>(owner);
        }

        public async Task<ResponseOwnerDTO> AddOwner(CreateOwnerDTO createOwnerDTO)
        {
            Owner mappedOwner = _mapper.Map<Owner>(createOwnerDTO);

            var newOwner = await _ownerRepository.AddOwner(mappedOwner);

            return _mapper.Map<ResponseOwnerDTO>(newOwner);
        }

        public async Task<ResponseOwnerDTO> UpdateOwner(UpdateOwnerDTO updateOwnerDTO)
        {
            Owner mappedOwner = _mapper.Map<Owner>(updateOwnerDTO);

            var updatedOwner = await _ownerRepository.UpdateOwner(mappedOwner);

            return _mapper.Map<ResponseOwnerDTO>(updatedOwner);
        }

        public async Task<bool> DeleteOwner(int id)
        {
            return await _ownerRepository.DeleteOwner(id);
        }
    }
}
