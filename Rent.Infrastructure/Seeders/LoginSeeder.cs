using Rent.Domain.Entities;
using Rent.Domain.Enums;
using Rent.Domain.Interfaces.Services;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Seeders
{
    public class LoginSeeder
    {
        private readonly DataContext _context;

        private readonly ISecurityService _securityService;

        public LoginSeeder(DataContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }

        public void Seed()
        {
            // Verifica se já existem registros no banco de dados
            if (_context.Logins.Any())
            {
                return; // Se já existir, não faz nada
            }

            // Insere os registros iniciais
            var login = new Login
            {
                ParentId = _context.Owners.First().Id,
                Email = "admin@email.com",
                CreatedAt = DateTime.Now,
                Password = "admin",
                UserType = UserType.Owner
            };

            var hash = _securityService.HashPassword(login.Password, out var salt);

            login.PasswordHash = hash;
            login.PasswordSalt = salt;

            _context.Logins.Add(login);
            _context.SaveChanges();
        }
    }
}
