namespace PioConnection.Api.Dtos;

public class ApiResult<T>
{
    public T? Data { get; set; }

    public bool IsSuccess { get; set; }

    public IEnumerable<ErrorInfo>? Errors { get; set; }
}

public class ErrorInfo
{
    public string? Message { get; set; }
}