namespace PioConnection.Commands.Abstractions;

/// <summary>
/// A command to be executed on the solver that doesn't
/// return any values 
/// </summary>
public interface ISolverNonQuery : ISolverCommand
{
    /// <summary>
    /// Executes this command on the solver
    /// </summary>
    void Execute();
}