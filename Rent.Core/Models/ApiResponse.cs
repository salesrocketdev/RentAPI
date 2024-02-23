namespace Rent.Core.Models
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public PaginationMeta? Pagination { get; set; }

        public ApiResponse()
        {
            Data = default!;
        }
    }
}
