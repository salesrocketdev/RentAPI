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
    [Route("api/v1/[controller]")]
    [SwaggerTag("Clientes")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Retorna todos os clientes cadastrados no sistema.",
            Description = "Este endpoint retorna uma lista de clientes cadastrados no sistema."
        )]
        public async Task<ActionResult<List<ResponseCustomerDTO>>> GetAllCustomers(
            int pageNumber = 1,
            int pageSize = 10
        )
        {
            try
            {
                var (customers, pagination) = await _customerService.GetAllCustomers(
                    pageNumber,
                    pageSize
                );
                List<ResponseCustomerDTO> customerDTOs = _mapper.Map<List<ResponseCustomerDTO>>(
                    customers
                );

                ApiResponse<List<ResponseCustomerDTO>> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = customerDTOs,
                        Pagination = pagination
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<List<ResponseCustomerDTO>> response =
                    new() { Code = 0, Message = ex.Message, };

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
                Customer customer = await _customerService.GetCustomerById(id);
                ResponseCustomerDTO customerDTO = _mapper.Map<ResponseCustomerDTO>(customer);

                ApiResponse<ResponseCustomerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = customerDTO
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseCustomerDTO> response =
                    new() { Code = 0, Message = ex.Message };

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
                Customer mappedCustomer = _mapper.Map<Customer>(createCustomerDTO);

                Customer createdCustomer = await _customerService.AddCustomer(mappedCustomer);

                var response = new
                {
                    Code = 1,
                    Message = "Success",
                    Data = _mapper.Map<CreateCustomerDTO>(createdCustomer)
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new { Code = 0, Message = ex.Message, };

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
                Customer mappedCustomer = _mapper.Map<Customer>(updateCustomerDTO);

                Customer updatedCustomer = await _customerService.UpdateCustomer(mappedCustomer);

                ApiResponse<UpdateCustomerDTO> response =
                    new()
                    {
                        Code = 1,
                        Message = "Success.",
                        Data = _mapper.Map<UpdateCustomerDTO>(updatedCustomer)
                    };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<ResponseCustomerDTO> response =
                    new() { Code = 0, Message = ex.Message, };

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
                ApiResponse<ResponseCustomerDTO> response =
                    new() { Code = 0, Message = ex.Message };

                return BadRequest(response);
            }
        }
    }
}
