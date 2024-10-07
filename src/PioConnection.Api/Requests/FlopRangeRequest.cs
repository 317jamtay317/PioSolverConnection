using System.Text;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class FlopRangeRequest : RangeRequest
{
    /// <inheritdoc cref="RangeRequest.Street"/>
    public override Street Street => Street.Flop;

    /// <summary>
    /// Gets or sets a list of <see cref="PioConnection.Dtos.PlayerAction"/> that allows us to know
    /// the action for the flop up to this point, if this is null we would be on
    /// the OOP player and no actions have happened. 
    /// </summary>
    public IEnumerable<PlayerAction>? FlopActions { get; set; } = [];

    /// <inheritdoc cref="RangeRequest.BuildNodeString"/>
    public override string BuildNodeString()
    {
        if (FlopActions is null)
        {
            throw new ArgumentNullException(nameof(FlopActions), "FlopActions cannot be null");
        }
        var stringBuilder = new StringBuilder("r:0");
        foreach (var playerAction in FlopActions)
        {
            stringBuilder.Append($":{playerAction}");
        }
        return stringBuilder.ToString();
    }
}