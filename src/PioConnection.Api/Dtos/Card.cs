using PioConnection.Api.Extensions;

namespace PioConnection.Api.Dtos;

/// <summary>
/// A structure to represent a playing card
/// </summary>
public struct Card : IEquatable<Card>
{
    public Card(Suit suit, FaceValue faceValue)
    {
        Suit = suit;
        FaceValue = faceValue;
    }
    
    /// <summary>
    /// Gets the Suit of the card
    /// </summary>
    public Suit Suit { get; init; }

    /// <summary>
    /// Gets the Face value of the card
    /// </summary>
    public FaceValue FaceValue { get; init; }

    /// <summary>
    /// overrides the <see cref="Object.ToString"/> to display the
    /// correct string that represents the card 
    /// </summary>
    public override string ToString() =>
        $"{FaceValue.ToDisplayString()}{Suit.ToDisplayString()}";

    /// <inheritdoc cref="IEquatable{T}.Equals"/>
    public bool Equals(Card other) =>
        Suit == other.Suit && FaceValue == other.FaceValue;

    /// <inheritdoc cref="IEquatable{T}.Equals"/>
    public override bool Equals(object? obj) =>
        obj is Card other && Equals(other);

    /// <inheritdoc cref="IEquatable{T}.GetHashCode"/>
    public override int GetHashCode() =>
        HashCode.Combine((int)Suit, (int)FaceValue);
}