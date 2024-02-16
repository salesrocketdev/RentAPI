using Microsoft.AspNetCore.Http;

namespace Rent.Domain;

public class CreateCarImageDTO
{
    public int CarId { get; set; }
    public IFormFile? Image { get; set; }
    public bool IsPrimary { get; set; }
}
