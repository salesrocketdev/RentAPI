using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
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
        private readonly IMapper _mapper;

        public CustomerService(
            ISecurityService securityService,
            ILoginRepository loginRepository,
            ICustomerRepository customerRepository,
            IMapper mapper
        )
        {
            _securityService = securityService;
            _loginRepository = loginRepository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<ResponsePaginateDTO<ResponseCustomerDTO>> GetAllCustomers(
            int pageNumber,
            int pageSize
        )
        {
            (List<Customer>, PaginationMeta) customers = await _customerRepository.GetAllCustomers(
                pageNumber,
                pageSize
            );

            var (Data, PaginationMeta) = customers;

            ResponsePaginateDTO<ResponseCustomerDTO> responsePaginateDTO =
                new()
                {
                    Data = _mapper.Map<List<ResponseCustomerDTO>>(Data),
                    PaginationMeta = PaginationMeta
                };

            return responsePaginateDTO;
        }

        public async Task<ResponseCustomerDTO> GetCustomerById(int id)
        {
            Customer customer = await _customerRepository.GetCustomerById(id);

            return _mapper.Map<ResponseCustomerDTO>(customer);
        }

        public async Task<ResponseCustomerDTO> AddCustomer(CreateCustomerDTO createCustomerDTO)
        {
            Customer? customerVerification = _customerRepository.Find(
                x =>
                    x.Email == createCustomerDTO.Email
                    || x.Document.TaxNumber == createCustomerDTO.Document.TaxNumber,
                x => x.Document
            );

            if (customerVerification != null)
            {
                if (createCustomerDTO.Document.TaxNumber == customerVerification.Document.TaxNumber)
                    throw new Exception("O cpf já está em uso.");

                if (createCustomerDTO.Email == customerVerification.Email)
                    throw new Exception("O email já está em uso.");
            }

            // var password = _securityService.GenerateRandomPassword(8);
            string password = "12345678";

            var hash = _securityService.HashPassword(password, out var salt);

            Customer mappedCustomer = _mapper.Map<Customer>(createCustomerDTO);

            var newCustomer = await _customerRepository.AddCustomer(mappedCustomer);

            Login login =
                new()
                {
                    Email = createCustomerDTO.Email,
                    UserType = Enums.UserType.Customer,
                    ParentId = newCustomer.Id,
                    PasswordHash = hash,
                    PasswordSalt = salt,
                    CreatedAt = DateTime.UtcNow
                };

            await _loginRepository.AddLogin(login);

            return _mapper.Map<ResponseCustomerDTO>(newCustomer);
        }

        public async Task<ResponseCustomerDTO> UpdateCustomer(UpdateCustomerDTO updateCustomerDTO)
        {
            Customer mappedCustomer = _mapper.Map<Customer>(updateCustomerDTO);

            var updatedCustomer = await _customerRepository.UpdateCustomer(mappedCustomer);

            return _mapper.Map<ResponseCustomerDTO>(updatedCustomer);
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            return await _customerRepository.DeleteCustomer(id);
        }
    }
}
