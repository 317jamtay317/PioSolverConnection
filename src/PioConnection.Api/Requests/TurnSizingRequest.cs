using PioConnection.Commands.Builders;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class TurnSizingRequest : FlopSizingRequest
{
    /// <summary>
    /// Gets or sets the OOP player actions
    /// </summary>
    public IEnumerable<PlayerAction>? OOPPlayerTurnActions { get; set; }

    /// <summary>
    /// Gets or sets the IP player actions
    /// </summary>
    public IEnumerable<PlayerAction>? IPPlayerTurnActions { get; set; }

    /// <summary>
    /// Gets or sets the turn card
    /// </summary>
    public Card Turn { get; set; }

    public override string BuildNodeString()
    {
        NodeStringBuilder builder = new();
        builder.WithOOPFlopActions(OOPPlayerFlopActions)
            .WithIPFlopActions(IPPlayerFlopActions)
            .WithOOPTurnActions(OOPPlayerTurnActions)
            .WithTurnCard(Turn);
        return builder.ToString();
    }
}