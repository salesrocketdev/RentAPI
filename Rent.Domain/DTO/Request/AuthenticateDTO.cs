namespace Rent.Domain.DTO.Request
{
    public class AuthenticateDTO
    {
        public string? Email { get; set; } = "admin@email.com";
        public string? Password { get; set; } = "admin";
    }
}
