using System.Collections.Concurrent;
using Client.Util;

namespace PioConnection.Commands.Abstractions;

/// <summary>
/// The base class that represents a basic command. 
/// </summary>
public abstract class SolverCommand(string stringCommand, SolverConnection connection) 
    : ISolverCommand
{
    /// <inheritdoc cref="ISolverCommand.SolverConnection"/>
    public ISolverConnection SolverConnection { get; } = connection;

    /// <inheritdoc cref="ISolverCommand.Arguments"/>
    public IReadOnlyDictionary<string, object> Arguments => ArgumentsDictionary;

    /// <inheritdoc cref="ISolverCommand.CommandName"/>
    public string CommandName { get; } = stringCommand;

    /// <inheritdoc cref="ISolverCommand.AddArgument"/>
    public virtual void AddArgument(string key, string value)
    {
        ArgumentsDictionary.TryAdd(key, value);
    }

    protected object[] GetArguments()
    {
        return Arguments
            .Select(x => x.Value)
            .Cast<object>()
            .ToArray();
    }

    protected ConcurrentDictionary<string, object> ArgumentsDictionary = new();
}