using FluentAssertions;
using PioConnection.Dtos.Extensions;

namespace PioConnection.Dtos.Tests.Unit.Extensions;

public class PlayerPositionExtenstions
{
    [Theory]
    [InlineData(PlayerPosition.UTG, 0)]
    [InlineData(PlayerPosition.UTG1, 1)]
    [InlineData(PlayerPosition.LJ, 2)]
    [InlineData(PlayerPosition.HJ, 3)]
    [InlineData(PlayerPosition.CO, 4)]
    [InlineData(PlayerPosition.BTN, 5)]
    [InlineData(PlayerPosition.SB, 6)]
    [InlineData(PlayerPosition.BB, 7)]
    public void PreflopOrder_ShouldReturnCorrectNumber_AccordingToPLayerPosition(PlayerPosition position, int expectedValue)
    {
        //arrange

        //act
        var value = position.PreflopOrder();
        //assert
        value.Should().Be(expectedValue);
    }
}