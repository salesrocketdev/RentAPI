using Microsoft.AspNetCore.Mvc;
using Rent.Domain.Entities;
using AutoMapper;
using Rent.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Rent.Domain.DTO.Response;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;

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
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers(
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
                List<CustomerDTO> customerDTOs = _mapper.Map<List<CustomerDTO>>(customers);

                ApiResponse<List<CustomerDTO>> response = new ApiResponse<List<CustomerDTO>>
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
                ApiResponse<List<CustomerDTO>> response = new ApiResponse<List<CustomerDTO>>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return Ok(response);
            }
        }

        [Authorize(Roles = "Owner, Employee")]
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Obter cliente por ID.",
            Description = "Retorna um cliente específico com base no seu ID."
        )]
        public async Task<ActionResult<CustomerDTO>> GetCustomerById(int id)
        {
            try
            {
                Customer customer = await _customerService.GetCustomerById(id);
                CustomerDTO customerDTO = _mapper.Map<CustomerDTO>(customer);

                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = customerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo cliente.",
            Description = "Cria um novo cliente."
        )]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CreateCustomerDTO customerRequest)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerRequest);

                Customer addedCustomer = await _customerService.AddCustomer(customer);

                CreateCustomerDTO addedCustomerDTO = _mapper.Map<CreateCustomerDTO>(addedCustomer);

                ApiResponse<CreateCustomerDTO> response = new ApiResponse<CreateCustomerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = addedCustomerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner, Customer")]
        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um cliente existente.",
            Description = "Atualiza um cliente existente."
        )]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(UpdateCustomerDTO customerRequest)
        {
            try
            {
                Customer customer = _mapper.Map<Customer>(customerRequest);

                Customer updatedCustomer = await _customerService.UpdateCustomer(customer);

                UpdateCustomerDTO updatedCustomerDTO = _mapper.Map<UpdateCustomerDTO>(updatedCustomer);

                ApiResponse<UpdateCustomerDTO> response = new ApiResponse<UpdateCustomerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                    Data = updatedCustomerDTO
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
                {
                    Code = 0,
                    Message = ex.Message,
                };

                return BadRequest(response);
            }
        }

        [Authorize(Roles = "Owner")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remover cliente por ID.",
            Description = "Remove um cliente específico com base no seu ID."
        )]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomer(id);

                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
                {
                    Code = 1,
                    Message = "Success.",
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
                {
                    Code = 0,
                    Message = ex.Message
                };

                return BadRequest(response);
            }
        }
    }
}
