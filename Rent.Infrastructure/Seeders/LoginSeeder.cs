using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rent.Domain.Entities;
using Rent.Domain.Enums;
using Rent.Domain.Interfaces.Services;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Seeders
{
    public class LoginSeeder
    {
        private readonly DataContext _context;

        private readonly IAuthenticationService _authenticationService;

        public LoginSeeder(DataContext context, IAuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }

        public void Seed()
        {
            byte[] salt;
            byte[] hash;

            // Verifica se já existem registros no banco de da dos
            if (_context.Logins.Any())
            {
                return; // Se já existir, não faz nada
            }

            // Insere os registros iniciais
            var login = new Login
            {
                ParentId = 1,
                Email = "user1@example.com",
                // PasswordHash = _authenticationService.HashPassword("admin123", out salt, out hash), // Faça o hash da senha aqui
                UserType = UserType.Owner
            };
        }
    }
}
