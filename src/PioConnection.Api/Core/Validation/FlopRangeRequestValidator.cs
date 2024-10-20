using FluentValidation;
using PioConnection.Api.Requests;
using PioConnection.Dtos;

namespace PioConnection.Api.Core.Validation;

public class FlopRangeRequestValidator : AbstractValidator<FlopRangeRequest>
{
    public FlopRangeRequestValidator()
    {
        RuleFor(x => x.Flop)
            .NotNull()
            .WithMessage("Flop is required");
        RuleFor(r => r.Flop)
            .Must(f => f.IsValidFlop())
            .WithMessage("The Flop must have 3 unique cards.");
        RuleFor(r => r.StackSize)
            .Must(ss=> ss != StackSize.NotDefined)
            .WithMessage("Please add the stack size to the request, this helps up find the file location.");
        RuleFor(r => r.GameType)
            .Must(gt=> gt != GameType.NotDefined)
            .WithMessage("Please add the game type to the request, this helps up find the file location.");
        RuleFor(r => r)
            .Must(NoSameFlopRaseSizes)
            .WithMessage("You cannot the same raise size in both players actions");
        RuleFor(r => r)
            .Must(VerifyPlayerPositions)
            .WithMessage("Please check the positions of the players");
    }
    /// <summary>
    /// Verifies the players positions are not out of order and the IP player actualy acts last
    /// </summary>
    /// <param name="arg">The flop request</param>
    /// <returns>True if the OOP player acts first, otherwise false.</returns>
    internal static bool VerifyPlayerPositions(SolverRequest arg) =>
        arg.IPPlayerPosition > arg.OOPPlayerPosition;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="arg"></param>
    /// <returns></returns>
    internal static bool VerifyIpPlayersLastActionNotCheck(IEnumerable<PlayerAction> arg)
        => arg.LastOrDefault() != PlayerAction.Check();

    /// <summary>
    /// Verifies the ip players last action not call.
    /// </summary>
    /// <param name="arg">The argument.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    internal static bool VerifyIpPlayersLastActionNotCall(IEnumerable<PlayerAction> arg)
        => arg.LastOrDefault() != PlayerAction.Call();

    /// <summary>
    /// Noes the same flop rase sizes.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    internal static bool NoSameFlopRaseSizes(FlopRangeRequest request)
    {
        var ipRaiseActions = request.IpFlopPlayerActions.Where(x => x.ActionType == ActionType.Raise).ToArray();
        var oopRaiseActions = request.OOPFlopPlayerActions.Where(x => x.ActionType == ActionType.Raise).ToArray();

        return ipRaiseActions.All(x => oopRaiseActions.All(y => x != y));
    }
}