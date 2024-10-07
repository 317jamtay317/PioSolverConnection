using Client.Plugins;
using Client.Util;
using PioConnection.Commands.Abstractions;

namespace PioConnection.Commands;

public class LoadTreeCommand : SolverCommand
{
    private readonly SolverMetadata _metadata;

    public LoadTreeCommand(ISolverConnection connection) : base(connection)
    {
    }

    public LoadTreeCommand(SolverMetadata metadata) : base(metadata)
    {
        _metadata = metadata;
    }

    public object[] Execute()
    {
        if (_metadata is null)
            throw new ArgumentNullException(nameof(_metadata));
        return base.Execute(CommandRequest.LoadTree(), _metadata.TreePath);
    }

    public object[] Execute(string treePath)
    {
        return base.Execute(CommandRequest.LoadTree(), treePath);
    }
}