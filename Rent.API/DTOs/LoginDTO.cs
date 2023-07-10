using Rent.Domain.Entities;

namespace Rent.API.DTOs
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
