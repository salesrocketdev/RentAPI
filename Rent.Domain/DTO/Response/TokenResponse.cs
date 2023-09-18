namespace Rent.Domain.DTO.Response
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
