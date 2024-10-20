using FluentValidation;
using PioConnection.Api.Requests;
using PioConnection.Dtos;

namespace PioConnection.Api.Core.Validation;

public class FlopSizingRequestValidator : AbstractValidator<FlopSizingRequest>
{
    public FlopSizingRequestValidator()
    {
        RuleFor(x => x.Flop)
            .NotNull()
            .WithMessage("Flop is required");
        RuleFor(x => x.Flop)
            .Must(x => x.IsValidFlop())
            .WithMessage("The Flop must have 3 unique cards.");
        RuleFor(x => x)
            .Must(x=>x.Street == Street.Flop)
            .WithMessage("The street must be a flop.");
        RuleFor(x => x.GameType)
            .Must(x => x != GameType.NotDefined)
            .WithMessage("Please add the game type to the request, this helps up find the file location.");
        RuleFor(r => r)
            .Must(NoSameFlopRaseSizes)
            .WithMessage("You cannot the same raise size in both players actions");
        RuleFor(x => x)
            .Must(FlopRangeRequestValidator.VerifyPlayerPositions)
            .WithMessage("Please check the positions of the players");
    }

    /// <summary>
    /// Noes the same flop rase sizes.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    internal static bool NoSameFlopRaseSizes(FlopSizingRequest request)
    {
        var ipRaiseActions = request.IPPlayerFlopActions.Where(x => x.ActionType == ActionType.Raise).ToArray();
        var oopRaiseActions = request.OOPPlayerFlopActions.Where(x => x.ActionType == ActionType.Raise).ToArray();

        return ipRaiseActions.All(x => oopRaiseActions.All(y => x != y));
    }
}