using Rent.Core.Models;

namespace Rent.Domain.DTO.Response;

public class ResponsePaginateDTO<T>
{
    public List<T>? Data { get; set; }
    public PaginationMeta? PaginationMeta { get; set; }
}
