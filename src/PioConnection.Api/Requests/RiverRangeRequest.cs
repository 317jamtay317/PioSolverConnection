using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

/// <summary>
/// A request to get the range from the river
/// </summary>
public class RiverRangeRequest : TurnRangeRequest
{
    /// <inheritdoc cref="SolverRequest.Street"/>
    public override Street Street => Street.River;

    /// <summary>
    /// Gets or sets a list of <see cref="PioConnection.Dtos.Tests.Unit.PlayerAction"/> that allows us to know
    /// the action for the river up to this point, if this is null we would be on
    /// the OOP player and no actions have happened. 
    /// </summary>
    public IEnumerable<PlayerAction>? RiverActions { get; set; }
    
    /// <summary>
    /// A card to represent the last card to be shown in the hand
    /// </summary>
    public Card? RiverCard { get; set; }

    /// <inheritdoc cref="SolverRequest.BuildNodeString"/>
    public override string BuildNodeString() =>
        throw new NotImplementedException();
}