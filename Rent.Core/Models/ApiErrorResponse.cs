namespace Rent.Core.Models;

public class ApiErrorResponse<T>
{
    public int Code { get; set; }
    public string? Message { get; set; }
}
