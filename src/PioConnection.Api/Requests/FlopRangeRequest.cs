using System.Text;
using Newtonsoft.Json;
using PioConnection.Api.Core.Builders;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class FlopRangeRequest : RangeRequest
{
    public override string FilePath
    {
        get
        {
            var filePathBuilder = new FilePathBuilder()
                .IsTournament()
                .PotType(PotType.SRP)
                .WithPositions(PlayerPosition.UTG, PlayerPosition.BTN)
                .WithStackDepth(10)
                .WithFlop(Flop.FistCard, Flop.SecondCard, Flop.ThirdCard)
                .ToString();
            return filePathBuilder;//TODO: figure out configuration and builder information
        }
    }

    /// <inheritdoc cref="RangeRequest.Street"/>
    public override Street Street => Street.Flop;

    /// <summary>
    /// Gets or sets a list of <see cref="PioConnection.Dtos.Tests.Unit.PlayerAction"/> that allows us to know
    /// the action for the flop up to this point, if this is null we would be on
    /// the OOP player and no actions have happened. 
    /// </summary>
    public IEnumerable<PlayerAction>? FlopActions { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the flop cards 
    /// </summary>
    public Flop Flop { get; set; }

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