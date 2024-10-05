namespace PioConnection.Api.Logging;

public interface ILoggerWrapper<T>
    where T:class
{
    /// <summary>
    /// Logs the passed in message with the info severity
    /// </summary>
    void Info(string message);

    /// <summary>
    /// Logs the passed in message with the Warning severity
    /// </summary>
    void Warning(string message);

    /// <summary>
    /// Logs the passed in message with the Error Severity
    /// </summary>
    /// <param name="message"></param>
    void Error(string message);
}