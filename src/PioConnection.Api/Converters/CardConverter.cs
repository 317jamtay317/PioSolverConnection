using Newtonsoft.Json;
using System;
using PioConnection.Api.Dtos;
using PioConnection.Api.Extensions;

namespace PioConnection.Api.Converters;

public class CardConverter : JsonConverter<Card>
{
    public override void WriteJson(JsonWriter writer, Card value, JsonSerializer serializer)
    {
        writer.WriteValue($"{value.FaceValue.ToDisplayString()}{value.Suit.ToDisplayString()}");
    }

    public override Card ReadJson(JsonReader reader, Type objectType, Card existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        // Move to the correct token
        if (reader.TokenType == JsonToken.Null)
        {
            throw new JsonSerializationException("Unexpected null token when reading a Card.");
        }

        if (reader.TokenType != JsonToken.String)
        {
            reader.Read(); // Ensure we are reading the string
        }

        var stringValue = reader.Value?.ToString(); // Now read the value directly

        if (string.IsNullOrEmpty(stringValue))
        {
            throw new JsonSerializationException("Expected a string value for the Card.");
        }

        // Assuming the format is "As" -> Ace of Spades
        var splitValue = stringValue.ToCharArray().Select(x=>x.ToString());
        if (!EnumHelper.TryParse(splitValue.First(), out FaceValue faceValue))
        {
            throw new JsonSerializationException("Expected a string value for the FaceValue of the card.");
        }

        if (!EnumHelper.TryParse(splitValue.Last(), out Suit suit))
        {
            throw new JsonSerializationException("Expected a string value for the Suit of the card.");
        }

        return new Card(suit, faceValue);
    }
}