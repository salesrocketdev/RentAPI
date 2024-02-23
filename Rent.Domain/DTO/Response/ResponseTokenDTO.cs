namespace Rent.Domain.DTO.Response
{
    public class ResponseTokenDTO
    {
        public string? Token { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
