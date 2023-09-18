using Rent.Domain.Entities;
using Rent.Domain.Interfaces.Services;
using Rent.Infrastructure.Data;

namespace Rent.Infrastructure.Seeders
{
    public class OwnerSeeder
    {
        private readonly DataContext _context;

        public OwnerSeeder(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Verifica se já existem registros no banco de dados
            if (_context.Owners.Any())
            {
                return; // Se já existir, não faz nada
            }

            // Insere os registros iniciais
            var owner = new Owner
            {
                Name = "Admin",
                Age = 23,
                Address = "Rua Enésio Rosa dos Santos",
                Phone = "21979720760",
                Email = "admin@email.com",
                CreatedAt = DateTime.Now,
            };

            _context.Owners.Add(owner);
            _context.SaveChanges();
        }
    }
}
