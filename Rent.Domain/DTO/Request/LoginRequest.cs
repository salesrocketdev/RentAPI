using Rent.Domain.Enums;

namespace Rent.Domain.DTO.Request
{
    public class LoginRequest
    {
        public int UserId { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public UserType userType { get; set; }
    }
}
