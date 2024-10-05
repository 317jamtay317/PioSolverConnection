using FluentAssertions;
using PioConnection.Api.Dtos;
using PioConnection.Api.Extensions;

namespace PioConnection.Api.Tests.Unit.Extensions;

public class SuitExtensionsTests
{
    [Theory]
    [InlineData(Suit.Clubs, "c")]
    [InlineData(Suit.Diamonds,"d")]
    [InlineData(Suit.Hearts, "h")]
    [InlineData(Suit.Spades, "s")]
    public void ToDisplayString_ShouldReturnCorrectValue(Suit suit, string expectedValue)
    {
        //arrange

        //act
        var result = suit.ToDisplayString();
        
        //assert
        result.Should().Be(expectedValue);
    }
}