using Client.Plugins;
using Client.Util;

namespace PioConnection.Api.Core;

public class SolverConnectionFactory : ISolverConnectionFactory
{
    public ISolverConnection Create(SolverMetadata metadata) =>
        new SolverConnection(metadata.SolverPath);
}

public interface ISolverConnectionFactory
{
    ISolverConnection Create(SolverMetadata metadata);
}