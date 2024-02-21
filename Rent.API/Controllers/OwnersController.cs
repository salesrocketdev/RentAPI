using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Rent.API.Controllers
{
    [Authorize(Roles = "Owner")]
    [Route("api/v1/[controller]")]
    [SwaggerTag("Administradores")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os admnistradores cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de admnistradores cadastrados no sistema."
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

                ApiResponse<List<OwnerDTO>> response = new ApiResponse<List<OwnerDTO>>()
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
                ApiResponse<List<OwnerDTO>> response = new ApiResponse<List<OwnerDTO>>()
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter administrador por ID.",
            Description = "Retorna um administrador específico com base no seu ID."
        )]
        public async Task<ActionResult<OwnerDTO>> GetOwnerById(int id)
        {
            try
            {
                Owner owner = await _ownerService.GetOwnerById(id);
                OwnerDTO ownerDTO = _mapper.Map<OwnerDTO>(owner);

                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>()
                {
                    Code = 1,
                    Message = "Success.",
                    Data = ownerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>()
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo administrador.",
            Description = "Cria um novo administrador."
        )]
        public async Task<ActionResult<OwnerDTO>> CreateOwner(CreateOwnerDTO ownerRequest)
        {
            try
            {
                Owner owner = _mapper.Map<Owner>(ownerRequest);

                Owner addedOwner = await _ownerService.AddOwner(owner);

                CreateOwnerDTO addedOwnerDTO = _mapper.Map<CreateOwnerDTO>(addedOwner);

                ApiResponse<CreateOwnerDTO> response = new ApiResponse<CreateOwnerDTO>()
                {
                    Code = 1,
                    Message = "Success.",
                    Data = addedOwnerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>()
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um administrador existente.",
            Description = "Atualiza um administrador existente."
        )]
        public async Task<ActionResult<OwnerDTO>> UpdateOwner(UpdateOwnerDTO ownerRequest)
        {
            try
            {
                Owner owner = _mapper.Map<Owner>(ownerRequest);

                Owner updatedOwner = await _ownerService.UpdateOwner(owner);

                UpdateOwnerDTO updatedOwnerDTO = _mapper.Map<UpdateOwnerDTO>(updatedOwner);

                ApiResponse<UpdateOwnerDTO> response = new ApiResponse<UpdateOwnerDTO>()
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedOwnerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>()
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover administrador por ID.",
            Description = "Remove um administrador específico com base no seu ID."
        )]
        public async Task<ActionResult<OwnerDTO>> DeleteOwner(int id)
        {
            try
            {
                await _ownerService.DeleteOwner(id);

                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>()
                {
                    Code = 1,
                    Message = "Success.",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<OwnerDTO> response = new ApiResponse<OwnerDTO>()
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}
