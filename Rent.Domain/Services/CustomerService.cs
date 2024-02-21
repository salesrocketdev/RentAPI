using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Repositories;
using Rent.Domain.Interfaces.Services;

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
            Customer? customerVerification = _customerRepository.Find(
                x =>
                    x.Email == customer.Email
                    || x.Document.TaxNumber == customer.Document.TaxNumber,
                x => x.Document
            );

            if (customerVerification != null)
            {
                if (customer.Document.TaxNumber == customerVerification.Document.TaxNumber)
                    throw new Exception("O cpf já está em uso.");

                if (customer.Email == customerVerification.Email)
                    throw new Exception("O email já está em uso.");
            }

            var password = _securityService.GenerateRandomPassword(8);
            var hash = _securityService.HashPassword(password, out var salt);

            var newCustomer = await _customerRepository.AddCustomer(customer);

            Login login =
                new()
                {
                    Email = customer.Email,
                    UserType = Enums.UserType.Customer,
                    ParentId = newCustomer.Id,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.UtcNow
                };

            await _loginRepository.AddLogin(login);

            return newCustomer;
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
