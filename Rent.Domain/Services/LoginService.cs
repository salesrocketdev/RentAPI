using AutoMapper;
using Rent.Core.Models;
using Rent.Domain.Entities;
using Rent.Domain.Interfaces;

namespace Rent.Domain.Services
{
    public class LoginService : ILoginService
    {
        private readonly ICustomerService _customerService;
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public LoginService(
            ICustomerService customerService,
            ILoginRepository loginRepository,
            IMapper mapper
        )
        {
            _customerService = customerService;
            _loginRepository = loginRepository;
            _mapper = mapper;
        }

        public async Task<(List<Login>, PaginationMeta)> GetAllLogins(int pageNumber, int pageSize)
        {
            return await _loginRepository.GetAllLogins(pageNumber, pageSize);
        }

        public async Task<Login> GetLoginById(int id)
        {
            return await _loginRepository.GetLoginById(id);
        }

        public async Task<Login> AddLogin(Login login)
        {
            if (login.UserType == Enums.UserType.Owner)
            {
                var owner = await _customerService.GetCustomerById(login.ParentId);

                if (owner == null)
                    throw new Exception("Owner não existe.");
            }

            if (login.UserType == Enums.UserType.Employee)
            {
                var employee = await _customerService.GetCustomerById(login.ParentId);

                if (employee == null)
                    throw new Exception("Employee não existe.");
            }

            if (login.UserType == Enums.UserType.Customer)
            {
                var customer = await _customerService.GetCustomerById(login.ParentId);

                if (customer == null)
                    throw new Exception("Customer não existe.");
            }

            return await _loginRepository.AddLogin(login);
        }

        public async Task<Login> UpdateLogin(Login login)
        {
            return await _loginRepository.UpdateLogin(login);
        }

        public async Task DeleteLogin(int id)
        {
            await _loginRepository.DeleteLogin(id);
        }
    }
}
