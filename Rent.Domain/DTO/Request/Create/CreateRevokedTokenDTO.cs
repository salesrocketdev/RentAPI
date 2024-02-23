namespace Rent.Domain.DTO.Request.Create;

public class CreateRevokedTokenDTO
{
    public string? TokenId { get; set; }
    public DateTime ExpirationDate { get; set; }
}
