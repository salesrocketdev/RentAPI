using Microsoft.AspNetCore.Mvc;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using AutoMapper;
using Rent.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;
using Rent.API.DTOs;

namespace Rent.API.Controllers
{
    [Authorize]
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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar um novo cliente.",
            Description = "Cria um novo cliente."
        )]
        public async Task<ActionResult<CustomerDTO>> CreateCustomer(CustomerDTO customerRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Customer customer = _mapper.Map<Customer>(customerRequest);

                // Adicionar o carro usando o serviço
                Customer addedCustomer = await _customerService.AddCustomer(customer);

                // Mapear o carro adicionado de volta para CarDTO
                CustomerDTO addedCustomerDTO = _mapper.Map<CustomerDTO>(addedCustomer);

                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
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

        [HttpPut]
        [SwaggerOperation(
            Summary = "Atualiza um cliente existente.",
            Description = "Atualiza um cliente existente."
        )]
        public async Task<ActionResult<CustomerDTO>> UpdateCustomer(CustomerDTO customerRequest)
        {
            try
            {
                // Mapear a CarDTO para a entidade Car
                Customer customer = _mapper.Map<Customer>(customerRequest);

                // Adicionar o carro usando o serviço
                Customer updatedCustomer = await _customerService.UpdateCustomer(customer);

                // Mapear o carro adicionado de volta para CarDTO
                CustomerDTO updatedCustomerDTO = _mapper.Map<CustomerDTO>(updatedCustomer);

                ApiResponse<CustomerDTO> response = new ApiResponse<CustomerDTO>
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
