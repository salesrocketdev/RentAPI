using Rent.Core.Models;

namespace Rent.Domain.DTO.Response;

public class ResponseBrandDTO : BaseDTO
{
    public string? Name { get; set; }
    public string? BrandImage { get; set; }
    public int Quantity { get; set; }
}
