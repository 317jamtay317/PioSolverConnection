using System.Text;
using Newtonsoft.Json;
using PioConnection.Api.Core.Builders;
using PioConnection.Commands.Builders;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public class FlopRangeRequest : SolverRequest
{
    [JsonIgnore]
    public override string FilePath
    {
        get
        {
            if (Flop is null || !Flop.Any()) 
                throw new ArgumentException("The flop cards are required");
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

    /// <inheritdoc cref="SolverRequest.Street"/>
    public override Street Street => Street.Flop;

    /// <summary>
    /// Gets or sets the OOP player actions for the flop. See Remarks...
    /// </summary>
    /// <remarks>
    /// The order of actions must be in the order the player played them.
    /// For example: if the hand is BTN vs BB the BB leads 25, the button raises,
    /// and the bb 3 bets to 200. The <see cref="OOPFlopPlayerActions"/> would contain
    /// 2 items
    ///  - <see cref="PlayerAction"/> with <see cref="PlayerAction.ActionType"/> = Bet and <see cref="PlayerAction.Size"/> = 25
    ///  - <see cref="PlayerAction"/> with <see cref="PlayerAction.ActionType"/> = Raise and <see cref="PlayerAction.Size"/> = 200
    /// </remarks>
    public IEnumerable<PlayerAction> OOPFlopPlayerActions { get; set; } = [];

    /// <summary>
    /// Gets or sets the OOP player actions for the flop. See Remarks...
    /// </summary>
    /// <remarks>
    /// The order of actions must be in the order the player played them.
    /// For example: if the hand is BTN vs BB the BB leads 25, the button raises to 100,
    /// and the bb 3 bets to 200, the BTN calls. The <see cref="IpFlopPlayerActions"/> would contain
    /// 2 items
    ///  - <see cref="PlayerAction"/> with <see cref="PlayerAction.ActionType"/> = Raise and <see cref="PlayerAction.Size"/> = 100
    ///  - <see cref="PlayerAction"/> with <see cref="PlayerAction.ActionType"/> = Call and <see cref="PlayerAction.Size"/> = null
    /// </remarks>
    public IEnumerable<PlayerAction> IpFlopPlayerActions { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the flop cards.
    /// you can use a string array of card representation for this.
    /// example: ["As","Ac","Ad"] would be the flop AsAcAd  
    /// </summary>
    public Flop? Flop { get; set; }

    /// <inheritdoc cref="SolverRequest.BuildNodeString"/>
    public override string BuildNodeString()
    {
        NodeStringBuilder builder = new NodeStringBuilder();

        foreach (PlayerAction oopPlayerAction in OOPFlopPlayerActions)
        {
            builder.WithOOPFlopAction(oopPlayerAction);
        }

        foreach (PlayerAction ipPlayerAction in IpFlopPlayerActions)
        {
            builder.WithIPFlopAction(ipPlayerAction);
        }

        return builder.ToString();
    }
}