using Client.Plugins;
using Client.Util;
using PioConnection.Commands.Abstractions;
using PioConnection.Dtos;

namespace PioConnection.Commands;

public class RangeCommand : SolverCommand
{
    public RangeCommand(ISolverConnection connection) : base(connection)
    {
    }

    public RangeCommand(SolverMetadata metadata) : base(metadata)
    {
    }

    public Dictionary<Street, IEnumerable<PlayerAction>> Actions { get; } = new();
    
    public string NodeString { get; init; }
    public object[] Execute()
    {
        if (NodeString is null)
            throw new ArgumentNullException(nameof(NodeString), "NodeString is required to run a command");
        return base.Execute(CommandRequest.ShowHumanReadableStratigy(), "r:0");
    }
}