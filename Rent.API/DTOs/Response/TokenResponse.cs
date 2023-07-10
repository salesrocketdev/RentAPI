namespace Rent.API.DTOs.Response
{
    public class TokenResponse
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
