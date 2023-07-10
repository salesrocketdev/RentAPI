namespace Rent.Domain.Entities
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
