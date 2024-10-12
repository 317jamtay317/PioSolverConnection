using System.Collections.Concurrent;
using Client.Plugins;
using Client.Util;
using Microsoft.Extensions.Configuration;

namespace PioConnection.Commands.Abstractions;

/// <summary>
/// The base class that represents a basic command. 
/// </summary>
public abstract class SolverCommand : ISolverCommand
{
    /// <summary>
    /// The base class that represents a basic command. 
    /// </summary>
    public SolverCommand(ISolverConnection connection)
    {
        SolverConnection = connection;
    }

    public SolverCommand(SolverMetadata metadata)
    {
        SolverConnection = new SolverConnection(metadata.SolverPath);;
    }

    /// <inheritdoc cref="ISolverCommand.SolverConnection"/>
    public ISolverConnection SolverConnection { get; }

    /// <inheritdoc cref="ISolverCommand.Execute"/>
    internal virtual string[] Execute(CommandRequest request, params object[] args)
    {
        string?[] argsAsString = args.Where(x=>x is not null).Select(x => x.ToString()).ToArray();
        return SolverConnection.GetResponseFromSolver($"{request} {string.Join(' ', argsAsString)}");
    }
}