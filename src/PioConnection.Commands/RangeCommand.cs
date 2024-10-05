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

    public RangeCommand(RangeMetadata metadata) : base(metadata)
    {
    }

    public ICollection<PlayerAction> Actions { get; } = new List<PlayerAction>();
    public object[] Execute()
    {
        return base.Execute(CommandRequest.ShowHumanReadableStratigy(), "r:0");
    }
}