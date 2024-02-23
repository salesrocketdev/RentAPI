using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;

namespace Rent.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<ResponsePaginateDTO<ResponseCustomerDTO>> GetAllCustomers(
            int pageNumber,
            int pageSize
        );
        Task<ResponseCustomerDTO> GetCustomerById(int id);
        Task<ResponseCustomerDTO> AddCustomer(CreateCustomerDTO createCustomerDTO);
        Task<ResponseCustomerDTO> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO);
        Task<bool> DeleteCustomer(int id);
    }
}
