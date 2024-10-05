namespace PioConnection.Api.Dtos;
/// <summary>
/// Information on an error that happened
/// </summary>
public record ErrorInfo(string Message)
{
    /// <summary>
    /// Gets the message that descibes the error
    /// </summary>
    public string Message { get; init; } = Message;

    public void Deconstruct(out string Message)
    {
        Message = this.Message;
    }
}