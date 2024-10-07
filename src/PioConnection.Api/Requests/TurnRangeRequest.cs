using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class TurnRangeRequest : FlopRangeRequest
{
    /// <inheritdoc cref="RangeRequest.Street"/>
    public override Street Street => Street.Turn;

    /// <summary>
    /// Gets or sets a list of <see cref="PioConnection.Dtos.PlayerAction"/> that allows us to know
    /// the action for the turn up to this point, if this is null we would be on
    /// the OOP player and no actions have happened. 
    /// </summary>
    public IEnumerable<PlayerAction>? TurnActions { get; set; }
    
    /// <summary>
    /// Gets or sets the second community card shown
    /// </summary>
    public Card? TurnCard { get; set; }

    /// <inheritdoc cref="RangeRequest.BuildNodeString"/>
    public override string BuildNodeString() =>
        throw new NotImplementedException();
}