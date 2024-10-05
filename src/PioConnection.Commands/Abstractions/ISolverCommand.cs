using Client.Util;

namespace PioConnection.Commands.Abstractions;

/// <summary>
/// A command to help describe a SolverCommand
/// </summary>
public interface ISolverCommand
{
    /// <summary>
    /// The connection to the solver
    /// </summary>
    ISolverConnection SolverConnection { get; }
    
    /// <summary>
    /// The arguments for this command
    /// </summary>
    IReadOnlyDictionary<string, object> Arguments { get; }
    
    /// <summary>
    /// the solver name of this command
    /// </summary>
    string CommandName { get; }
    
    /// <summary>
    /// Adds an argument to the command
    /// </summary>
    /// <param name="key">The string name of the argument</param>
    /// <param name="value">The value to be sent to the solver</param>
    void AddArgument(string key, string value);
}