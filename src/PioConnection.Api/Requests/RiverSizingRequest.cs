using PioConnection.Commands.Builders;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class RiverSizingRequest : TurnSizingRequest
{
    /// <summary>
    /// Gets or sets the OOP Player river actions
    /// </summary>
    public IEnumerable<PlayerAction>? OOPPlayerRiverActions { get; set; }

    /// <summary>
    /// Gets or sets the IP player river actions
    /// </summary>
    public IEnumerable<PlayerAction>? IPPlayerRiverActions { get; set; }

    /// <summary>
    /// Gets or sets the River card
    /// </summary>
    public Card River { get; set; }

    public override string BuildNodeString()
    {
        NodeStringBuilder builder = new();
        builder
            .WithOOPFlopActions(OOPPlayerFlopActions)
            .WithIPFlopActions(IPPlayerFlopActions)
            .WithOOPTurnActions(OOPPlayerTurnActions)
            .WithIPTurnActions(IPPlayerTurnActions)
            .WithTurnCard(Turn)
            .WithOOPRiverActions(OOPPlayerRiverActions)
            .WithIPRiverActions(IPPlayerRiverActions)
            .WithRiverCard(River);
        return builder.ToString();
    }
}