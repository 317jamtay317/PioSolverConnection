namespace PioConnection.Commands.Abstractions;

/// <summary>
/// An interface to help represent a command to query the solver
/// </summary>
public interface ISolverQueryCommand : ISolverCommand
{
    /// <summary>
    /// Executes this command and gets a value from the solver
    /// </summary>
    /// <returns>The result from the solver.</returns>
    string[] Execute();
}