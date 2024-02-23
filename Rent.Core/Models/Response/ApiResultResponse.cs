namespace Rent.Core.Response.Result;

public class ApiResultResponse<T>
{
    public int Code { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}
