using PioConnection.Commands.Builders;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class FlopSizingRequest : SolverRequest
{
    /// <inheritdoc cref="SolverRequest.Street"/>
    public override Street Street => Street.Flop;
    
    /// <summary>
    /// Gets or sets the list of <see cref="PlayerAction"/> by the OOP player
    /// </summary>
    public IEnumerable<PlayerAction> OOPPlayerFlopActions { get; set; }
    
    /// <summary>
    /// Gets or sets the list of <see cref="PlayerAction"/> by the IP player
    /// </summary>
    public IEnumerable<PlayerAction> IPPlayerFlopActions { get; set; }

    public override string BuildNodeString()
    {
        if (Flop is null || !Flop.IsValidFlop())
        {
            throw new ArgumentException("The flop is not specified correctly, please make sure that the flop contains three uniqe cards");
        }

        NodeStringBuilder builder = new();
        builder.WithOOPFlopActions(OOPPlayerFlopActions)
            .WithIPFlopActions(IPPlayerFlopActions);
        return builder.ToString();
    }
}