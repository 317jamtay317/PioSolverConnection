using System.Collections;
using FluentAssertions;
using FluentValidation;
using PioConnection.Api.Core.Validation;
using PioConnection.Api.Requests;
using PioConnection.Dtos;

namespace PioConnection.Api.Tests.Unit.Core.Validation;

public class TurnRangeRequestValidatorTests
{
    [Fact]
    public void Validate_ShouldFail_WhenNoOOPFlopActions()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            GameType = GameType.Tournaments,
            Flop = [Card.AceClubs(), Card.AceDiamonds(), Card.AceHearts()],
            OOPFlopPlayerActions = [],
            IpFlopPlayerActions = [PlayerAction.Check()],
            StackSize = StackSize._20,
            TurnCard = Card.TwoClubs(),
            IPPlayerPosition = PlayerPosition.BTN,
            OOPPlayerPosition = PlayerPosition.BB,
            OOPPlayerTurnActions = [],
            IPPlayerTurnActions = []
        };
        //act
        var validationResult = _sut.Validate(request);
        //assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors.First().ErrorMessage.Should().Be("The turn request must have oop players flop actions.");
    }
    [Fact]
    public void Validate_ShouldFail_WhenNoIPFlopActions()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            GameType = GameType.Tournaments,
            Flop = [Card.AceClubs(), Card.AceDiamonds(), Card.AceHearts()],
            OOPFlopPlayerActions = [PlayerAction.Check()],
            IpFlopPlayerActions = [],
            StackSize = StackSize._20,
            TurnCard = Card.TwoClubs(),
            IPPlayerPosition = PlayerPosition.BTN,
            OOPPlayerPosition = PlayerPosition.BB,
            OOPPlayerTurnActions = [],
            IPPlayerTurnActions = []
        };
        //act
        var validationResult = _sut.Validate(request);
        //assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors.First().ErrorMessage.Should().Be("The turn request must have IP players flop actions.");
    }
    [Fact]
    public void Validate_ShouldFail_WhenNoGameType()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            GameType = default,
            Flop = [Card.AceClubs(),Card.AceDiamonds(), Card.AceHearts()],
            OOPFlopPlayerActions = [PlayerAction.Check()],
            IpFlopPlayerActions = [PlayerAction.Check()],
            StackSize = StackSize._20,
            TurnCard = Card.TwoClubs(),
            IPPlayerPosition = PlayerPosition.BTN,
            OOPPlayerPosition = PlayerPosition.BB,
            OOPPlayerTurnActions = [],
            IPPlayerTurnActions = []
        };
        //act
        var validationResult = _sut.Validate(request);
        //assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors.First().ErrorMessage.Should().Be("Please add the game type to the request, this helps up find the file location.");
    }

    [Fact]
    public void Validate_ShouldFail_WhenFlopIsInvalid()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            GameType = GameType.Cash,
            Flop = [Card.AceClubs(),Card.AceDiamonds()],
            OOPFlopPlayerActions = [PlayerAction.Check()],
            IpFlopPlayerActions = [PlayerAction.Check()],
            StackSize = StackSize._20,
            TurnCard = Card.TwoClubs(),
            IPPlayerPosition = PlayerPosition.BTN,
            OOPPlayerPosition = PlayerPosition.BB,
            OOPPlayerTurnActions = [],
            IPPlayerTurnActions = []
        };
        //act
        var validationResult = _sut.Validate(request);
        //assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors.First().ErrorMessage.Should().Be("The Flop must have 3 unique cards.");
    }
    [Fact]
    public void Validate_ShouldFail_WhenStacKSizeIsNotDefined()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            GameType = GameType.Cash,
            Flop = [Card.AceClubs(),Card.AceDiamonds(),Card.EightDiamonds()],
            OOPFlopPlayerActions = [PlayerAction.Check()],
            IpFlopPlayerActions = [PlayerAction.Check()],
            StackSize = StackSize.NotDefined,
            TurnCard = Card.TwoClubs(),
            IPPlayerPosition = PlayerPosition.BTN,
            OOPPlayerPosition = PlayerPosition.BB,
            OOPPlayerTurnActions = [],
            IPPlayerTurnActions = []
        };
        //act
        var validationResult = _sut.Validate(request);
        //assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors.First().ErrorMessage
            .Should()
            .Be("Please add the stack size to the request, this helps up find the file location.");
    }
    [Fact]
    public void Validate_ShouldFail_WhenGameTypeIsNotDefined()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            GameType = GameType.NotDefined,
            Flop = [Card.AceClubs(),Card.AceDiamonds(),Card.EightDiamonds()],
            OOPFlopPlayerActions = [PlayerAction.Check()],
            IpFlopPlayerActions = [PlayerAction.Check()],
            StackSize = StackSize._100,
            TurnCard = Card.TwoClubs(),
            IPPlayerPosition = PlayerPosition.BTN,
            OOPPlayerPosition = PlayerPosition.BB,
            OOPPlayerTurnActions = [],
            IPPlayerTurnActions = []
        };
        //act
        var validationResult = _sut.Validate(request);
        //assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should().HaveCount(1);
        validationResult.Errors.First().ErrorMessage
            .Should()
            .Be("Please add the game type to the request, this helps up find the file location.");
    }

    [Fact]
    public void VerifyTurnCard_ShouldReturnFalse_WhenCardIsNull()
    {
        //arrange
        
        //act
        var result = TurnRangeRequestValidator.VerifyTurnCard((Card?)null);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void VerifyTurnCard_ShouldFail_WhenDefaultTurnCard()
    {
        //arrange

        //act
        var result = TurnRangeRequestValidator.VerifyTurnCard(default);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void VerifyTurnCard_ShouldSucced_WhenValidCard()
    {
        //arrange

        //act
        var result = TurnRangeRequestValidator.VerifyTurnCard(Card.AceClubs());
        //assert
        result.Should().BeTrue();
    }

    [Fact]
    public void NoSameTurnSizes_ShouldFail_WhenThereIsTheSameBetSizeForIpAsOOP()
    {
        //arrange
        TurnRangeRequest rangeRequest = new()
        {
            OOPPlayerTurnActions = [PlayerAction.Bet(25) ],
            IPPlayerTurnActions = [PlayerAction.Bet(25) ]
        };
        //act
        var result = TurnRangeRequestValidator.NoSameTurnSizes(rangeRequest);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void NoSameTurnSizes_ShouldFail_WhenThereIsTheSameBetOrRaiseSizeForIpAsOOP()
    {
        //arrange
        TurnRangeRequest rangeRequest = new()
        {
            OOPPlayerTurnActions = [PlayerAction.Bet(25) ],
            IPPlayerTurnActions = [PlayerAction.Raise(25) ]
        };
        //act
        var result = TurnRangeRequestValidator.NoSameTurnSizes(rangeRequest);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void NoSameTurnSizes_ShouldPass_WhenDifferentSizes()
    {
        //arrange
        TurnRangeRequest rangeRequest = new()
        {
            OOPPlayerTurnActions = [PlayerAction.Bet(25) ],
            IPPlayerTurnActions = [PlayerAction.Raise(100) ]
        };
        //act
        var result = TurnRangeRequestValidator.NoSameTurnSizes(rangeRequest);
        //assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyUniqueTurnCard_ShouldFail_WhenThereIsAnExistingCardInFlop()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            Flop = [Card.AceSpades(), Card.KingSpades(), Card.QueenSpades()],
            TurnCard = Card.AceSpades()
        };
        //act
        var result = TurnRangeRequestValidator.VerifyUniqueTurnCard(request);
        //assert
        result.Should().BeFalse();
    }

    [Fact]
    public void VerifyUniqueTurnCard_ShouldPass_WhenUniqueTurnCard()
    {
        //arrange
        TurnRangeRequest request = new()
        {
            Flop = [Card.AceSpades(), Card.KingSpades(), Card.QueenSpades()],
            TurnCard = Card.TenSpades()
        };
        //act
        var result = TurnRangeRequestValidator.VerifyUniqueTurnCard(request);
        //assert
        result.Should().BeTrue();
    }

    [Fact]
    public void VerifyIPPlayersLastActionIsCheckOrCall_ShouldFail_WhenTheLastActionIsBet()
    {
        //arrange
        PlayerAction[] actions = [PlayerAction.Bet(25), PlayerAction.Raise(102)];
        //act
        var result = TurnRangeRequestValidator.VerifyIPPlayersLastActionIsCheckOrCall(actions);
        //assert
        result.Should().BeFalse();
    }
    [Fact]
    public void VerifyIPPlayersLastActionIsCheckOrCall_ShouldPass_WhenTheLastActionIsBet()
    {
        //arrange
        PlayerAction[] actions = [PlayerAction.Bet(25), PlayerAction.Call()];
        //act
        var result = TurnRangeRequestValidator.VerifyIPPlayersLastActionIsCheckOrCall(actions);
        //assert
        result.Should().BeTrue();
    }
    private readonly TurnRangeRequestValidator _sut = new();
}