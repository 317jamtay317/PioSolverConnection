using Client.Plugins;
using Client.Util;
using Microsoft.Extensions.Configuration;
using PioConnection.Commands.Abstractions;
using PioConnection.Dtos;

namespace PioConnection.Commands;
/// <summary>
/// A command to send to the solver to get a range
/// </summary>
public class RangeCommand : SolverCommand
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="connection">The connection to be passed in</param>
    public RangeCommand(ISolverConnection connection) : base(connection)
    {
    }

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="metadata">Metadata to create the connection</param>
    public RangeCommand(SolverMetadata metadata, IConfiguration configuration) : base(metadata)
    {
    }

    /// <summary>
    /// Gets a list of actions up to the point in the hand that were trying to get
    /// </summary>
    public Dictionary<Street, IEnumerable<PlayerAction>> Actions { get; } = new();
    
    /// <summary>
    /// Gets or sets a string that represents the node that we're on
    /// </summary>
    public string NodeString { get; set; }
    
    public object[] Execute()
    {
        if (Actions.Any())
        {
            NodeString = BuildNodeString();
        }
        if (NodeString is null)
            throw new ArgumentNullException(nameof(NodeString), "NodeString is required to run a command");
        return base.Execute(CommandRequest.ShowHumanReadableStratigy(), NodeString);
    }

    private string BuildNodeString()
    {
        throw new NotImplementedException();
    }
}