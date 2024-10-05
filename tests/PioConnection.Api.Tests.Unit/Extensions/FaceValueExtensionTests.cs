using FluentAssertions;
using PioConnection.Api.Dtos;
using PioConnection.Dtos;
using PioConnection.Dtos.Extensions;

namespace PioConnection.Api.Tests.Unit.Extensions;

public class FaceValueExtensionTests
{
    [Theory]
    [InlineData(FaceValue.Ace, "A")]
    [InlineData(FaceValue.King, "K")]
    [InlineData(FaceValue.Queen, "Q")]
    [InlineData(FaceValue.Jack, "J")]
    [InlineData(FaceValue.Ten, "T")]
    [InlineData(FaceValue.Nine, "9")]
    [InlineData(FaceValue.Eight, "8")]
    [InlineData(FaceValue.Seven, "7")]
    [InlineData(FaceValue.Six, "6")]
    [InlineData(FaceValue.Five, "5")]
    [InlineData(FaceValue.Four, "4")]
    [InlineData(FaceValue.Three, "3")]
    [InlineData(FaceValue.Two, "2")]
    public void ToDisplayString_ShouldReturnCorrectValue(FaceValue faceValue, string expectedValue)
    {
        //arrange

        //act
        var result = faceValue.ToDisplayString();
        
        //assert
        result.Should().Be(expectedValue);
    }
}