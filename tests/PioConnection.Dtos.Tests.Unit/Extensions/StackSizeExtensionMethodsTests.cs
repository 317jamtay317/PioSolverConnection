using FluentAssertions;
using PioConnection.Dtos.Extensions;

namespace PioConnection.Dtos.Tests.Unit.Extensions;

public class StackSizeExtensionMethodsTests
{
    [Theory]
    [InlineData(StackSize._10, "10BB")]
    [InlineData(StackSize._15, "15BB")]
    [InlineData(StackSize._20, "20BB")]
    [InlineData(StackSize._25, "25BB")]
    [InlineData(StackSize._30, "30BB")]
    [InlineData(StackSize._35, "35BB")]
    [InlineData(StackSize._40, "40BB")]
    [InlineData(StackSize._50, "50BB")]
    [InlineData(StackSize._60, "60BB")]
    [InlineData(StackSize._80, "80BB")]
    [InlineData(StackSize._100, "100BB")]
    [InlineData(StackSize._150, "150BB")]
    [InlineData(StackSize._200, "200BB")]
    public void ToFilePathString_ShouldReturnCorrectFilePathString(StackSize stackSize, string expectedValue)
    {
        //arrange

        //act
        var value = stackSize.ToFilePathString();
        
        //assert
        value.Should().Be(expectedValue);
    }

    [Fact]
    public void ToFilePathString_ShouldThorw_WhenInvalidNumber()
    {
        //arrange
        Action action = () => ((StackSize)100).ToFilePathString();
        //act

        //assert
        action.Should().Throw<ArgumentOutOfRangeException>();
    }
}