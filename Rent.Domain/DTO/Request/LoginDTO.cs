using Rent.Domain.Enums;

namespace Rent.Domain.DTO.Request
{
    public class LoginDTO
    {
        public int Id { get; set; }
        public int ParentId { get; set; }
        public UserType UserType { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
