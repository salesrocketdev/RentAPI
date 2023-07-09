using Microsoft.AspNetCore.Mvc;
using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using AutoMapper;
using Rent.API.DTOs;
using Rent.Core.Models;

namespace Rent.API.Controllers
{
    [Route("api/v1/[controller]")]
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
        public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomers(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
               var (customers, pagination) = await _customerService.GetAllCustomers(pageNumber, pageSize);
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
