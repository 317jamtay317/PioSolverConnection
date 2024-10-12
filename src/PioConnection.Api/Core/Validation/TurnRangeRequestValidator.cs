using FluentValidation;
using PioConnection.Api.Requests;
using PioConnection.Dtos;

namespace PioConnection.Api.Core.Validation;

public class TurnRangeRequestValidator : AbstractValidator<TurnRangeRequest>
{
    public TurnRangeRequestValidator()
    {
        RuleFor(x=>x.Flop)
            .Must(f=>f.IsValidFlop())
            .WithMessage("The Flop must have 3 unique cards.");
        RuleFor(r => r.StackSize)
            .Must(ss=> ss != StackSize.NotDefined)
            .WithMessage("Please add the stack size to the request, this helps up find the file location.");
        RuleFor(r => r.GameType)
            .Must(gt=> gt != GameType.NotDefined)
            .WithMessage("Please add the game type to the request, this helps up find the file location.");
        RuleFor(r => r)
            .Must(FlopRangeRequestValidator.NoSameFlopRaseSizes)
            .WithMessage("You cannot the same raise size in both players actions");
        RuleFor(x => x.TurnCard)
            .Must(VerifyTurnCard)
            .WithMessage("The turn card is required");
        RuleFor(x => x)
            .Must(VerifyUniqueTurnCard)
            .WithMessage("The turn must be unique from all other cards on the board");
        RuleFor(x => x)
            .Must(NoSameTurnSizes)
            .WithMessage("You cannot the same raise size in both players actions");
        RuleFor(x=>x.IPPlayerTurnActions)
            .Must(FlopRangeRequestValidator.VerifyIpPlayersLastActionNotCall)
            .WithMessage("The IP players last turn action is call, please add a turn card with a turn request.");
        RuleFor(x => x.IPPlayerTurnActions)
            .Must(FlopRangeRequestValidator.VerifyIpPlayersLastActionNotCheck)
            .WithMessage("The IP players last turn action is check, please add a turn card with a turn request.");
    }

    /// <summary>
    /// Verifies that the last action of the IP player is check or call
    /// </summary>
    /// <param name="arg">The items that you're checking</param>
    /// <returns>True if the last item is check or call, otherwise, false</returns>
    internal static bool VerifyIPPlayersLastActionIsCheckOrCall(IEnumerable<PlayerAction> arg) =>
        arg.Last() == PlayerAction.Call() || arg.Last() == PlayerAction.Check();

    /// <summary>
    /// Verifies that they Turn card does not match any of the flop cards
    /// </summary>
    /// <param name="arg">The turn request</param>
    /// <returns>True if the card is different than all the flop cards, otherwise, false</returns>
    internal static bool VerifyUniqueTurnCard(TurnRangeRequest arg)
    {
        return !arg.Flop.Any(x => x.Equals(arg.TurnCard));
    }
    
    /// <summary>
    /// Verifies that two players cannot bet $25 on the turn. See Remarks...
    /// </summary>
    /// <param name="request">The turn request</param>
    /// <returns>True if all bet/raise sizes are different, otherwise, false</returns>
    /// <remarks>
    /// if the OOP player bets $25 on the turn, the IP player cannot raise to $25,
    /// they can only call or raise larger than $25 
    /// </remarks>
    internal static bool NoSameTurnSizes(TurnRangeRequest request)
    {
        var ipRaiseActions = request
            .IPPlayerTurnActions
            .Where(x => x.ActionType == ActionType.Raise || x.ActionType == ActionType.Bet)
            .ToArray();
        var oopRaiseActions = request
            .OOPPlayerTurnActions
            .Where(x => x.ActionType == ActionType.Raise|| x.ActionType == ActionType.Bet)
            .ToArray();
        return ipRaiseActions.All(x => oopRaiseActions.All(y => x.Size != y.Size));
    }
    
    /// <summary>
    /// Verifies that the card passed in is not null or default.
    /// </summary>
    /// <param name="arg">The turn card</param>
    /// <returns>True if valid card, otherwise, false</returns>
    internal static bool VerifyTurnCard(Card? arg)
    {
        return arg.HasValue;
    }
}