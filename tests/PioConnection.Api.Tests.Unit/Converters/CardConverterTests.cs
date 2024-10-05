using FluentAssertions;
using Newtonsoft.Json;
using PioConnection.Api.Converters;
using PioConnection.Api.Dtos;
using PioConnection.Dtos;

namespace PioConnection.Api.Tests.Unit.Converters;

public class CardConverterTests
{
    [Fact]
    public void ReadJson_ShouldDeserializeCorrectly()
    {
        // Arrange
        var json = "\"As\""; // Ace of Spades
        var converter = new CardConverter();
        var jsonReader = new JsonTextReader(new StringReader(json));
        var serializer = new JsonSerializer();
        var expectedValue = new Card(Suit.Spades, FaceValue.Ace);

        // Act
        jsonReader.Read();
        var card = converter.ReadJson(jsonReader, typeof(Card), default(Card), false, serializer);

        // Assert
        card.Equals(expectedValue).Should().BeTrue();
    }

    [Fact]
    public void WriteJson_ShouldSerializeCorrectly()
    {
        // Arrange
        var card = new Card(Suit.Spades, FaceValue.Ace);
        var converter = new CardConverter();
        var stringWriter = new StringWriter();
        var jsonWriter = new JsonTextWriter(stringWriter);
        var serializer = new JsonSerializer();

        // Act
        converter.WriteJson(jsonWriter, card, serializer);
        jsonWriter.Flush();
        var json = stringWriter.ToString();

        // Assert
        json.Should().Be("\"As\""); // Expecting "As"
    }

    [Fact]
    public void ReadJson_InvalidFaceValue_ShouldThrowException()
    {
        // Arrange
        var json = "\"Xh\""; // Invalid FaceValue ("X" is not a valid FaceValue, but "h" is a valid Suit)
        var converter = new CardConverter();
        var jsonReader = new JsonTextReader(new StringReader(json));
        var serializer = new JsonSerializer();

        // Act
        jsonReader.Read();
        Action action = () => converter.ReadJson(jsonReader, typeof(Card), default(Card), false, serializer);

        // Assert
        action.Should().Throw<JsonSerializationException>()
            .WithMessage("Expected a string value for the FaceValue of the card.");
    }

    [Fact]
    public void ReadJson_InvalidSuit_ShouldThrowException()
    {
        // Arrange
        var json = "\"Af\""; // Invalid Suit ("f" is not a valid Suit in this example, assuming "s", "d", "c", and "h" are valid)
        var converter = new CardConverter();
        var jsonReader = new JsonTextReader(new StringReader(json));
        var serializer = new JsonSerializer();

        // Act
        jsonReader.Read();
        Action action = () => converter.ReadJson(jsonReader, typeof(Card), default(Card), false, serializer);

        // Assert
        action.Should().Throw<JsonSerializationException>()
            .WithMessage("Expected a string value for the Suit of the card.");
    }


    [Fact]
    public void ReadJson_ShouldThrowOnNullValue()
    {
        // Arrange
        var json = "null"; // Null value
        var converter = new CardConverter();
        var jsonReader = new JsonTextReader(new StringReader(json));
        var serializer = new JsonSerializer();

        // Act
        jsonReader.Read();
        Action action = () => converter.ReadJson(jsonReader, typeof(Card), default(Card), false, serializer);

        // Assert
        action.Should().Throw<JsonSerializationException>().WithMessage("Unexpected null token when reading a Card.");
    }

    [Fact]
    public void ReadJson_NonStringToken_ShouldThrowException()
    {
        // Arrange
        var json = "123"; // Non-string token
        var converter = new CardConverter();
        var jsonReader = new JsonTextReader(new StringReader(json));
        var serializer = new JsonSerializer();

        // Act
        jsonReader.Read();
        Action action = () => converter.ReadJson(jsonReader, typeof(Card), default(Card), false, serializer);

        // Assert
        action.Should().Throw<JsonSerializationException>().WithMessage("Expected a string value for the Card.");
    }
}