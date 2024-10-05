using Client.Util;

namespace PioConnection.Commands.Abstractions;

public abstract class SolverQueryCommand(string stringCommand, SolverConnection connection)
    :SolverCommand(stringCommand, connection), ISolverQueryCommand
{
    /// <inheritdoc cref="ISolverQueryCommand.Execute"/>
    public string[] Execute()
    {
        var result = connection.GetResponseFromSolver(CommandName, 
            GetArguments());
        return result;
    }
}