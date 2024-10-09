using PioConnection.Commands.Builders;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class TurnRangeRequest : FlopRangeRequest
{
    /// <inheritdoc cref="SolverRequest.Street"/>
    public override Street Street => Street.Turn;

    public IEnumerable<PlayerAction> OOPPlayerTurnActions { get; set; } = [];
    
    public IEnumerable<PlayerAction> IPPlayerTurnActions { get; set; } = [];

    /// <summary>
    /// Gets or sets the second community card shown
    /// </summary>
    public Card? TurnCard { get; set; }

    /// <inheritdoc cref="SolverRequest.BuildNodeString"/>
    public override string BuildNodeString()
    {
        var builder = new NodeStringBuilder();
        foreach (PlayerAction oopPlayerAction in OOPFlopPlayerActions)
        {
            builder.WithOOPFlopAction(oopPlayerAction);
        }

        foreach (PlayerAction ipPlayerAction in IpFlopPlayerActions)
        {
            builder.WithIPFlopAction(ipPlayerAction);
        }

        builder.WithTurnCard(TurnCard.Value);
        foreach (PlayerAction oopPlayerAction in OOPPlayerTurnActions)
        {
            builder.WithOOPTurnAction(oopPlayerAction);
        }

        foreach (PlayerAction ipPlayerAction in IPPlayerTurnActions)
        {
            builder.WithIPTurnAction(ipPlayerAction);
        }
        return builder.ToString();
    }
}