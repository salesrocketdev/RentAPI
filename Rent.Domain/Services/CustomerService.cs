using Rent.Domain.Interfaces;
using Rent.Domain.Entities;
using Rent.Core.Models;

namespace Rent.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<(List<Customer>, PaginationMeta)> GetAllCustomers(
            int pageNumber,
            int pageSize
        )
        {
            return await _customerRepository.GetAllCustomers(pageNumber, pageSize);
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _customerRepository.GetCustomerById(id);
        }

        public async Task<Customer> AddCustomer(Customer customer)
        {
            return await _customerRepository.AddCustomer(customer);
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.UpdateCustomer(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await _customerRepository.DeleteCustomer(id);
        }
    }
}
