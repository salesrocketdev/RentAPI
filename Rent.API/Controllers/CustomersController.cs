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
    [Route("api/v1/[controller]")]
    [SwaggerTag("Clientes")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os clientes cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de clientes cadastrados no sistema."
        )]
        public async Task<ActionResult<ResponsePaginateDTO<ResponseCustomerDTO>>> GetAllCustomers(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                ResponsePaginateDTO<ResponseCustomerDTO> responsePaginateDTO =
                    await _customerService.GetAllCustomers(pageNumber, pageSize);

                ApiResponse<List<ResponseCustomerDTO>> response =
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

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter cliente por ID.",
            Description = "Retorna um cliente específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseCustomerDTO>> GetCustomerById(int id)
        {
            try
            {
                ResponseCustomerDTO responseCustomerDTO = await _customerService.GetCustomerById(
                    id
                );

                ApiResultResponse<ResponseCustomerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = responseCustomerDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo cliente.",
            Description = "Cria um novo cliente."
        )]
        public async Task<ActionResult<ResponseCustomerDTO>> CreateCustomer(
            CreateCustomerDTO createCustomerDTO
        )
        {
            try
            {
                ResponseCustomerDTO createdCustomer = await _customerService.AddCustomer(
                    createCustomerDTO
                );

                ApiResultResponse<ResponseCustomerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success",
                        Data = createdCustomer
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Customer")]
        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um cliente existente.",
            Description = "Atualiza um cliente existente."
        )]
        public async Task<ActionResult<ResponseCustomerDTO>> UpdateCustomer(
            UpdateCustomerDTO updateCustomerDTO
        )
        {
            try
            {
                ResponseCustomerDTO updatedCustomer = await _customerService.UpdateCustomer(
                    updateCustomerDTO
                );

                ApiResultResponse<ResponseCustomerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = updatedCustomer
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiErrorResponse response = new() { Code = 0, Message = ex.Message, };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover cliente por ID.",
            Description = "Remove um cliente específico com base no seu ID."
        )]
        public async Task<ActionResult<ResponseCustomerDTO>> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomer(id);

                ApiResponse<ResponseCustomerDTO> response =
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
