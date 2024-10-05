namespace PioConnection.Api.Dtos;
/// <summary>
/// The response from the api
/// </summary>
/// <typeparam name="T">The type of data you are receiving</typeparam>
public class ApiResponse<T>
{
    /// <summary>
    /// Gets or sets the data
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets IsSuccess
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Gets or sets the Errors
    /// </summary>
    public IEnumerable<ErrorInfo>? Errors { get; set; }
}