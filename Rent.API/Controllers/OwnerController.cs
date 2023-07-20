using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTOs;
using Rent.Domain.DTOs.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Administrador")]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnerController(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os administradores cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de administradores cadastrados no sistema."
        )]
        public async Task<ActionResult<List<OwnerDTO>>> GetAllOwners(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var (owners, pagination) = await _ownerService.GetAllOwners(pageNumber, pageSize);
                List<OwnerDTO> ownerDTOs = _mapper.Map<List<OwnerDTO>>(owners);

                ApiResponse<List<OwnerDTO>> response = new ApiResponse<List<OwnerDTO>>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = ownerDTOs,
                    Pagination = pagination
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<List<OwnerDTO>> response = new ApiResponse<List<OwnerDTO>>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter funcionário por ID.",
            Description = "Retorna um funcionário específico com base no seu ID."
        )]
        public async Task<ActionResult<OwnerDTO>> GetOwnerById(int id)
        {
            try
            {
                Owner owner = await _ownerService.GetOwnerById(id);
                OwnerDTO ownerDTO = _mapper.Map<OwnerDTO>(owner);

                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = ownerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo funcionário.",
            Description = "Cria um novo funcionário."
        )]
        public async Task<ActionResult<OwnerDTO>> CreateOwner(OwnerDTO ownerRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Owner owner = _mapper.Map<Owner>(ownerRequest);

                // Adicionar o carro usando o serviço
                Owner addedOwner = await _ownerService.AddOwner(owner);

                // Mapear o carro adicionado de volta para CarDTO
                OwnerDTO addedOwnerDTO = _mapper.Map<OwnerDTO>(addedOwner);

                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = addedOwnerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um funcionário existente.",
            Description = "Atualiza um funcionário existente."
        )]
        public async Task<ActionResult<OwnerDTO>> UpdateOwner(OwnerDTO ownerRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Owner owner = _mapper.Map<Owner>(ownerRequest);

                // Adicionar o carro usando o serviço
                Owner updatedOwner = await _ownerService.UpdateOwner(owner);

                // Mapear o carro adicionado de volta para CarDTO
                OwnerDTO updatedOwnerDTO = _mapper.Map<OwnerDTO>(updatedOwner);

                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedOwnerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover funcionário por ID.",
            Description = "Remove um funcionário específico com base no seu ID."
        )]
        public async Task<ActionResult<OwnerDTO>> DeleteOwner(int id)
        {
            try
            {
                await _ownerService.DeleteOwner(id);

                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}
