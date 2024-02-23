using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rent.Core.Models;
using Rent.Core.Response.Result;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
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

        public OwnersController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os admnistradores cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de admnistradores cadastrados no sistema."
        )]
        public async Task<ActionResult<ResponsePaginateDTO<ResponseOwnerDTO>>> GetAllOwners(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                ResponsePaginateDTO<ResponseOwnerDTO> responsePaginateDTO =
                    await _ownerService.GetAllOwners(pageNumber, pageSize);

                ApiResponse<List<ResponseOwnerDTO>> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responsePaginateDTO.Data,
                        Pagination = responsePaginateDTO.PaginationMeta
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return Ok(response);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter administrador por ID.",
            Description = "Retorna um administrador específico com base no seu ID."
        )]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<ResponseOwnerDTO>> GetOwnerById(int id)
        {
            try
            {
                ResponseOwnerDTO responseOwnerDTO = await _ownerService.GetOwnerById(id);

                ApiResultResponse<ResponseOwnerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responseOwnerDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo administrador.",
            Description = "Cria um novo administrador."
        )]
        public async Task<ActionResult<ResponseOwnerDTO>> CreateOwner(CreateOwnerDTO createOwnerDTO)
        {
            try
            {
                ResponseOwnerDTO createdOwner = await _ownerService.AddOwner(createOwnerDTO);

                ApiResultResponse<ResponseOwnerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = createdOwner
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um administrador existente.",
            Description = "Atualiza um administrador existente."
        )]
        public async Task<ActionResult<ResponseOwnerDTO>> UpdateOwner(UpdateOwnerDTO updateOwnerDTO)
        {
            try
            {
                ResponseOwnerDTO updatedOwner = await _ownerService.UpdateOwner(updateOwnerDTO);

                ApiResultResponse<ResponseOwnerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = updatedOwner
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover administrador por ID.",
            Description = "Remove um administrador específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseOwnerDTO>> DeleteOwner(int id)
        {
            try
            {
                await _ownerService.DeleteOwner(id);

                ApiResultResponse<ResponseOwnerDTO> response =
                    new() { Code = 1, Message = "Success.", };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }
    }
}
