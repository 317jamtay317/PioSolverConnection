using System.Collections;
using FluentAssertions;
using PioConnection.Api.Core.Validation;
using PioConnection.Api.Requests;
using PioConnection.Dtos;

namespace PioConnection.Api.Tests.Unit.Core.Validation;

public class FlopRangeRequestValidatorTests
{
    [Fact]
    public void Validate_ShouldFail_WhenWeHaveAnInvalidFlop()
    {
        //arrange
        FlopRangeRequest request = new()
        {
            Flop = [Card.AceDiamonds(), Card.AceClubs()],
            GameType = GameType.Tournaments,
            IpFlopPlayerActions = [],
            OOPFlopPlayerActions = [],
            OOPPlayerPosition = PlayerPosition.BB,
            IPPlayerPosition = PlayerPosition.BTN,
            StackSize = StackSize._10
        };
        //act
        var result = _sut.Validate(request);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().ErrorMessage.Should().Be("The Flop must have 3 unique cards.");
    }

    [Fact]
    public void Validate_ShouldFail_WhenNoStackSizeIsEntered()
    {
        //arrange
        FlopRangeRequest request = new()
        {
            Flop = [Card.AceDiamonds(), Card.AceClubs(), Card.EightClubs()],
            GameType = GameType.Tournaments,
            IpFlopPlayerActions = [],
            OOPFlopPlayerActions = [],
            OOPPlayerPosition = PlayerPosition.BB,
            IPPlayerPosition = PlayerPosition.BTN,
            StackSize = StackSize.NotDefined
        };
        //act
        var result = _sut.Validate(request);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().ErrorMessage.Should().Be("Please add the stack size to the request, this helps up find the file location.");
    }
    [Fact]
    public void Validate_ShouldFail_WhenNoGameTypeIsEntered()
    {
        //arrange
        FlopRangeRequest request = new()
        {
            Flop = [Card.AceDiamonds(), Card.AceClubs(), Card.EightClubs()],
            GameType = GameType.NotDefined,
            IpFlopPlayerActions = [],
            OOPFlopPlayerActions = [],
            OOPPlayerPosition = PlayerPosition.BB,
            IPPlayerPosition = PlayerPosition.BTN,
            StackSize = StackSize._15
        };
        //act
        var result = _sut.Validate(request);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().ErrorMessage.Should().Be("Please add the game type to the request, this helps up find the file location.");
    }

    [Fact]
    public void Validate_ShouldFail_WhenOOPHasSameRaiseActionAsIP()
    {
        //arrange
        FlopRangeRequest request = new()
        {
            Flop = [Card.AceDiamonds(), Card.AceClubs(), Card.EightClubs()],
            GameType = GameType.Tournaments,
            IpFlopPlayerActions = [PlayerAction.Raise(25)],
            OOPFlopPlayerActions = [PlayerAction.Raise(25)],
            OOPPlayerPosition = PlayerPosition.BB,
            IPPlayerPosition = PlayerPosition.BTN,
            StackSize = StackSize._15
        };
        //act
        var result = _sut.Validate(request);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First()
            .ErrorMessage
            .Should()
            .Be("You cannot the same raise size in both players actions");
    }
    [Fact]
    public void Validate_ShouldFail_WhenPlayerPositionsAreNotInCorrectOrder()
    {
        //arrange
        FlopRangeRequest request = new()
        {
            Flop = [Card.AceDiamonds(), Card.AceClubs(), Card.EightClubs()],
            GameType = GameType.Tournaments,
            IpFlopPlayerActions = [PlayerAction.Raise(25)],
            OOPFlopPlayerActions = [],
            OOPPlayerPosition = PlayerPosition.CO,
            IPPlayerPosition = PlayerPosition.BB,
            StackSize = StackSize._15
        };
        //act
        var result = _sut.Validate(request);
        //assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First()
            .ErrorMessage
            .Should()
            .Be("Please check the positions of the players");
    }

    [Theory]
    [ClassData(typeof(VerifyPlayerPositionsTestData))]
    public void VerifyPlayerPositions_ShouldReturnCorrectValue_WithParametersGiven(PlayerPosition oopPlayer, PlayerPosition ipPlayer, bool isValid)
    {
        //arrange
        FlopRangeRequest request = new()
        {
            OOPPlayerPosition = oopPlayer,
            IPPlayerPosition = ipPlayer
        };
        //act
        var result = FlopRangeRequestValidator.VerifyPlayerPositions(request);
        //assert
        result.Should().Be(isValid);
    }

    [Theory]
    [ClassData(typeof(VerifyIpPlayersLastActionNotCheckTestData))]
    public void VerifyIpPlayersLastActionNotCheck_ShouldFaild_WhenPlayersActionIsCheck(PlayerAction[] ipPlayerActions, bool isValid)
    {
        //arrange
        //act
        var result = FlopRangeRequestValidator.VerifyIpPlayersLastActionNotCheck(ipPlayerActions);
        //assert
        result.Should().Be(isValid);
    }

    [Theory]
    [ClassData(typeof(VerifyIpPlayersLastActionNotCallTestData))]
    public void VerifyIpPlayersLastActionNotCall_ShouldFail_WhenPlayersLastActionIsCall(PlayerAction[] ipPlayerActions, bool isValid)
    {
        //arrange

        //act
        var result = FlopRangeRequestValidator.VerifyIpPlayersLastActionNotCall(ipPlayerActions);
        //assert
        result.Should().Be(isValid);
    }

    #region Data Classes

    public class VerifyPlayerPositionsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [PlayerPosition.UTG, PlayerPosition.UTG1, true];
            yield return [PlayerPosition.UTG, PlayerPosition.LJ, true];
            yield return [PlayerPosition.UTG, PlayerPosition.HJ, true];
            yield return [PlayerPosition.UTG, PlayerPosition.CO, true];
            yield return [PlayerPosition.UTG, PlayerPosition.BTN, true];
            yield return [PlayerPosition.UTG, PlayerPosition.SB, false];
            yield return [PlayerPosition.UTG, PlayerPosition.BB, false];
            yield return [PlayerPosition.UTG1, PlayerPosition.LJ, true];
            yield return [PlayerPosition.UTG1, PlayerPosition.HJ, true];
            yield return [PlayerPosition.UTG1, PlayerPosition.CO, true];
            yield return [PlayerPosition.UTG1, PlayerPosition.BTN, true];
            yield return [PlayerPosition.UTG1, PlayerPosition.SB, false];
            yield return [PlayerPosition.UTG1, PlayerPosition.BB, false];
            yield return [PlayerPosition.LJ, PlayerPosition.HJ, true];
            yield return [PlayerPosition.LJ, PlayerPosition.CO, true];
            yield return [PlayerPosition.LJ, PlayerPosition.BTN, true];
            yield return [PlayerPosition.LJ, PlayerPosition.SB, false];
            yield return [PlayerPosition.HJ, PlayerPosition.BB, false];
            yield return [PlayerPosition.HJ, PlayerPosition.CO, true];
            yield return [PlayerPosition.HJ, PlayerPosition.BTN, true];
            yield return [PlayerPosition.HJ, PlayerPosition.SB, false];
            yield return [PlayerPosition.HJ, PlayerPosition.BB, false];
            yield return [PlayerPosition.CO, PlayerPosition.BTN, true];
            yield return [PlayerPosition.CO, PlayerPosition.SB, false];
            yield return [PlayerPosition.CO, PlayerPosition.BB, false];
            yield return [PlayerPosition.BTN, PlayerPosition.SB, false];
            yield return [PlayerPosition.BTN, PlayerPosition.BB, false];
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
    
    public class VerifyIpPlayersLastActionNotCheckTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new []{PlayerAction.Check()}, false];
            yield return [new []{PlayerAction.Bet(25),PlayerAction.Call()}, true];
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
    
    public class VerifyIpPlayersLastActionNotCallTestData:IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return [new[] { PlayerAction.Bet(25) }, true];
            yield return [new[] { PlayerAction.Bet(25), PlayerAction.Call()}, false];
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }

    #endregion
    
    private readonly FlopRangeRequestValidator _sut = new();
}
