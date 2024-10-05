using FluentAssertions;
using PioConnection.Api.Converters;
using PioConnection.Api.Dtos;

namespace PioConnection.Api.Tests.Unit.Helpers;

public class EnumHelperTests
{
    [Fact]
    public void Parse_ShouldParseFaceValue_WhenPassedInValidString()
    {
        //arrange

        //act
        var result = EnumHelper.Parse<FaceValue>("T");
        
        //assert
        result.Should().Be(FaceValue.Ten);
    }

    [Fact]
    public void Parse_ShouldParseSuit_WhenPassedInValidString()
    {
        //arrange

        //act
        var result = EnumHelper.Parse<Suit>("d");
        
        //assert
        result.Should().Be(Suit.Diamonds);
    }

    [Fact]
    public void Parse_ShouldThrowException_WhenPassedInInvalidString()
    {
        //arrange
        Action action = () => EnumHelper.Parse<Suit>("f");
        //act

        //assert
        action.Should().Throw<ArgumentException>()
            .WithMessage("No matching enum found for value: f");
    }
}