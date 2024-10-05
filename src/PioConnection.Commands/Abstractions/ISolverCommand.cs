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
}