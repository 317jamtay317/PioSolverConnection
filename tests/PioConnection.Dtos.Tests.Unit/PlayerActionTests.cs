using FluentAssertions;

namespace PioConnection.Dtos.Tests.Unit;

public class PlayerActionTests
{
    [Fact]
    public void FromString_ShouldConvertNodeToAction_WithSingleAction()
    {
        //arrange
        var node = "r:0:b25";
        var splitNode = node.Split(":");
        PlayerAction expectedAction = PlayerAction.Bet(25);
        //act
        PlayerAction action = splitNode;
        //assert
        action.Equals(expectedAction).Should().BeTrue();
    }
    [Fact]
    public void Equals_ShouldBeTrue_WhenSameAction()
    {
        //arrange
        PlayerAction left = PlayerAction.Check();
        PlayerAction right = PlayerAction.Check();
        //act
        var isSame = left.Equals(right);
        //assert
        isSame.Should().BeTrue();
    }

    [Fact]
    public void Equals_ShouldBeFalse_WhenDifferent()
    {
        //arrange
        PlayerAction left = PlayerAction.Call();
        PlayerAction right = PlayerAction.Bet(25);
        //act
        var isSame = left.Equals(right);
        //assert
        isSame.Should().BeFalse();
    }

    [Fact]
    public void Equals_ShouldBeFalse_WhenBothAreBetButDifferentSize()
    {
        //arrange
        PlayerAction left = PlayerAction.Bet(50);
        PlayerAction right = PlayerAction.Bet(25);
        
        //act
        var isSame = left.Equals(right);
        //assert
        isSame.Should().BeFalse();
    }

    [Fact]
    public void EqualsOperator_ShouldBeTrue_WhenSameAction()
    {
        //arrange
        PlayerAction left = PlayerAction.Check();
        PlayerAction right = PlayerAction.Check();
        //act
        var isSame = left == right;
        //assert
        isSame.Should().BeTrue();
    }
    [Fact]
    public void EqualsOperator_ShouldBeFals_WhenDifferentAction()
    {
        //arrange
        PlayerAction left = PlayerAction.Check();
        PlayerAction right = PlayerAction.Call();
        //act
        var isSame = left == right;
        //assert
        isSame.Should().BeFalse();
    }
    [Fact]
    public void NotEqualsOperator_ShouldBeTrue_WhenDifferentAction()
    {
        //arrange
        PlayerAction left = PlayerAction.Check();
        PlayerAction right = PlayerAction.Call();
        //act
        var isSame = left != right;
        //assert
        isSame.Should().BeTrue();
    }
    [Fact]
    public void NotEqualsOperator_ShouldBeFalse_WhenSameAction()
    {
        //arrange
        PlayerAction left = PlayerAction.Check();
        PlayerAction right = PlayerAction.Check();
        //act
        var isSame = left != right;
        //assert
        isSame.Should().BeFalse();
    }
}