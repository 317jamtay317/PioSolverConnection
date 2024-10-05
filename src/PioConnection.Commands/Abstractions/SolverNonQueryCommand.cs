using Client.Util;

namespace PioConnection.Commands.Abstractions;

public abstract class SolverNonQueryCommand(string command, SolverConnection connection)
    : SolverCommand(command, connection), ISolverNonQuery
{
    /// <inheritdoc cref="ISolverNonQuery.Execute"/>
    public void Execute()
    {
        connection.GetResponseFromSolver(command, GetArguments());
    }
}