using Client.Plugins;
using Client.Util;
using PioConnection.Commands.Abstractions;

namespace PioConnection.Commands;

public class SizingCommand : SolverCommand
{
    public SizingCommand(ISolverConnection connection) : base(connection)
    {
    }

    public SizingCommand(SolverMetadata metadata) : base(metadata)
    {
    }
}