namespace PioConnection.Api.Logging;

public class LoggerWrapper<T>(ILogger<T> logger) : ILoggerWrapper<T>
    where T:class
{
    /// <summary>
    /// Logs the passed in message with the info severity
    /// </summary>
    public void Info(string message)
    {
        logger.LogInformation(message);
    }

    /// <summary>
    /// Logs the passed in message with the Warning severity
    /// </summary>
    public void Warning(string message)
    {
        logger.LogWarning(message);
    }

    /// <summary>
    /// Logs the passed in message with the Error Severity
    /// </summary>
    /// <param name="message"></param>
    public void Error(string message)
    {
        logger.LogError(message);
    }
}