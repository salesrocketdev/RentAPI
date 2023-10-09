using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;
using Rent.Domain.Entities;
using Rent.Core.Models;

namespace Rent.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ISecurityService _securityService;
        private readonly ILoginRepository _loginRepository;
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ISecurityService securityService,
            ILoginRepository loginRepository,
            ICustomerRepository customerRepository
        )
        {
            _securityService = securityService;
            _loginRepository = loginRepository;
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
            var password = _securityService.GenerateRandomPassword(8);
            var hash = _securityService.HashPassword(password, out var salt);

            var customerResult = await _customerRepository.AddCustomer(customer);

            Login login =
                new()
                {
                    Email = customer.Email,
                    UserType = Domain.Enums.UserType.Customer,
                    ParentId = customerResult.Id,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.UtcNow
                };

            await _loginRepository.AddLogin(login);
            return customerResult;
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            return await _customerRepository.AddCustomer(customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await _customerRepository.GetCustomerById(id);
        }
    }
}
