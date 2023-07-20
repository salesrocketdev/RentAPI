namespace Rent.Domain.DTOs.Response
{
    public class TokenResponseDTO
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
