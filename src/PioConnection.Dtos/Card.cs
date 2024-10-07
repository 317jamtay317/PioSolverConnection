using PioConnection.Dtos.Extensions;
using PioConnection.Dtos.Helpers;

namespace PioConnection.Dtos;

/// <summary>
/// A structure to represent a playing card
/// </summary>
public struct Card : IEquatable<Card>
{
    /// <summary>
    /// Returns the As card
    /// </summary>
    public static Card AceSpades() => new Card(Suit.Spades, FaceValue.Ace);
    /// <summary>
    /// Returns the Ac card
    /// </summary>
    public static Card AceClubs() => new Card(Suit.Clubs, FaceValue.Ace);
    /// <summary>
    /// Returns the Ah card
    /// </summary>
    public static Card AceHearts() => new Card(Suit.Hearts, FaceValue.Ace);
    /// <summary>
    /// Returns the Ad card
    /// </summary>
    public static Card AceDiamonds() => new Card(Suit.Diamonds, FaceValue.Ace);
    /// <summary>
    /// Returns the Ks card
    /// </summary>
    public static Card KingSpades() => new Card(Suit.Spades, FaceValue.King);
    /// <summary>
    /// Returns the Kc card
    /// </summary>
    public static Card KingClubs() => new Card(Suit.Clubs, FaceValue.King);
    /// <summary>
    /// Returns the Kh card
    /// </summary>
    public static Card KingHearts() => new Card(Suit.Hearts, FaceValue.King);
    /// <summary>
    /// Returns the Kd card
    /// </summary>
    public static Card KingDiamonds() => new Card(Suit.Diamonds, FaceValue.King);
    /// <summary>
    /// Returns the Qs card
    /// </summary>
    public static Card QueenSpades() => new Card(Suit.Spades, FaceValue.Queen);
    /// <summary>
    /// Returns the Qc card
    /// </summary>
    public static Card QueenClubs() => new Card(Suit.Clubs, FaceValue.Queen);
    /// <summary>
    /// Returns the Qh card
    /// </summary>
    public static Card QueenHearts() => new Card(Suit.Hearts, FaceValue.Queen);
    /// <summary>
    /// Returns the Qd card
    /// </summary>
    public static Card QueenDiamonds() => new Card(Suit.Diamonds, FaceValue.Queen);
    /// <summary>
    /// Returns the Js card
    /// </summary>
    public static Card JackSpades() => new Card(Suit.Spades, FaceValue.Jack);
    /// <summary>
    /// Returns the Jc card
    /// </summary>
    public static Card JackClubs() => new Card(Suit.Clubs, FaceValue.Jack);
    /// <summary>
    /// Returns the Jh card
    /// </summary>
    public static Card JackHearts() => new Card(Suit.Hearts, FaceValue.Jack);
    /// <summary>
    /// Returns the Jd card
    /// </summary>
    public static Card JackDiamonds() => new Card(Suit.Diamonds, FaceValue.Jack);
    /// <summary>
    /// Returns the Ts card
    /// </summary>
    public static Card TenSpades() => new Card(Suit.Spades, FaceValue.Ten);
    /// <summary>
    /// Returns the Tc card
    /// </summary>
    public static Card TenClubs() => new Card(Suit.Clubs, FaceValue.Ten);
    /// <summary>
    /// Returns the Th card
    /// </summary>
    public static Card TenHearts() => new Card(Suit.Hearts, FaceValue.Ten);
    /// <summary>
    /// Returns the Td card
    /// </summary>
    public static Card TenDiamonds() => new Card(Suit.Diamonds, FaceValue.Ten);
    /// <summary>
    /// Returns the 9s card
    /// </summary>
    public static Card NineSpades() => new Card(Suit.Spades, FaceValue.Nine);
    /// <summary>
    /// Returns the 9c card
    /// </summary>
    public static Card NineClubs() => new Card(Suit.Clubs, FaceValue.Nine);
    /// <summary>
    /// Returns the 9h card
    /// </summary>
    public static Card NineHearts() => new Card(Suit.Hearts, FaceValue.Nine);
    /// <summary>
    /// Returns the 9d card
    /// </summary>
    public static Card NineDiamonds() => new Card(Suit.Diamonds, FaceValue.Nine);
    /// <summary>
    /// Returns the 8s card
    /// </summary>
    public static Card EightSpades() => new Card(Suit.Spades, FaceValue.Eight);
    /// <summary>
    /// Returns the 8c card
    /// </summary>
    public static Card EightClubs() => new Card(Suit.Clubs, FaceValue.Eight);
    /// <summary>
    /// Returns the 8h card
    /// </summary>
    public static Card EightHearts() => new Card(Suit.Hearts, FaceValue.Eight);
    /// <summary>
    /// Returns the 8d card
    /// </summary>
    public static Card EightDiamonds() => new Card(Suit.Diamonds, FaceValue.Eight);
    /// <summary>
    /// Returns the 7s card
    /// </summary>
    public static Card SevenSpades() => new Card(Suit.Spades, FaceValue.Seven);
    /// <summary>
    /// Returns the 7c card
    /// </summary>
    public static Card SevenClubs() => new Card(Suit.Clubs, FaceValue.Seven);
    /// <summary>
    /// Returns the 7h card
    /// </summary>
    public static Card SevenHearts() => new Card(Suit.Hearts, FaceValue.Seven);
    /// <summary>
    /// Returns the 7d card
    /// </summary>
    public static Card SevenDiamonds() => new Card(Suit.Diamonds, FaceValue.Seven);
    /// <summary>
    /// Returns the 6s card
    /// </summary>
    public static Card SixSpades() => new Card(Suit.Spades, FaceValue.Six);
    /// <summary>
    /// Returns the 6c card
    /// </summary>
    public static Card SixClubs() => new Card(Suit.Clubs, FaceValue.Six);
    /// <summary>
    /// Returns the 6h card
    /// </summary>
    public static Card SixHearts() => new Card(Suit.Hearts, FaceValue.Six);
    /// <summary>
    /// Returns the 6d card
    /// </summary>
    public static Card SixDiamonds() => new Card(Suit.Diamonds, FaceValue.Six);
    /// <summary>
    /// Returns the 5s card
    /// </summary>
    public static Card FiveSpades() => new Card(Suit.Spades, FaceValue.Five);
    /// <summary>
    /// Returns the 5c card
    /// </summary>
    public static Card FiveClubs() => new Card(Suit.Clubs, FaceValue.Five);
    /// <summary>
    /// Returns the 5h card
    /// </summary>
    public static Card FiveHearts() => new Card(Suit.Hearts, FaceValue.Five);
    /// <summary>
    /// Returns the 5d card
    /// </summary>
    public static Card FiveDiamonds() => new Card(Suit.Diamonds, FaceValue.Five);
    /// <summary>
    /// Returns the 4s card
    /// </summary>
    public static Card FourSpades() => new Card(Suit.Spades, FaceValue.Four);
    /// <summary>
    /// Returns the 4c card
    /// </summary>
    public static Card FourClubs() => new Card(Suit.Clubs, FaceValue.Four);
    /// <summary>
    /// Returns the 4h card
    /// </summary>
    public static Card FourHearts() => new Card(Suit.Hearts, FaceValue.Four);
    /// <summary>
    /// Returns the 4d card
    /// </summary>
    public static Card FourDiamonds() => new Card(Suit.Diamonds, FaceValue.Four);
    /// <summary>
    /// Returns the 3s card
    /// </summary>
    public static Card ThreeSpades() => new Card(Suit.Spades, FaceValue.Three);
    /// <summary>
    /// Returns the 3c card
    /// </summary>
    public static Card ThreeClubs() => new Card(Suit.Clubs, FaceValue.Three);
    /// <summary>
    /// Returns the 3h card
    /// </summary>
    public static Card ThreeHearts() => new Card(Suit.Hearts, FaceValue.Three);
    /// <summary>
    /// Returns the 3d card
    /// </summary>
    public static Card ThreeDiamonds() => new Card(Suit.Diamonds, FaceValue.Three);
    /// <summary>
    /// Returns the 2s card
    /// </summary>
    public static Card TwoSpades() => new Card(Suit.Spades, FaceValue.Two);
    /// <summary>
    /// Returns the 2c card
    /// </summary>
    public static Card TwoClubs() => new Card(Suit.Clubs, FaceValue.Two);
    /// <summary>
    /// Returns the 2h card
    /// </summary>
    public static Card TwoHearts() => new Card(Suit.Hearts, FaceValue.Two);
    /// <summary>
    /// Returns the 2d card
    /// </summary>
    public static Card TwoDiamonds() => new Card(Suit.Diamonds, FaceValue.Two);
    public static implicit operator Card(string card)
    {
        var splitCard = card.ToCharArray().Select(x => x.ToString()).ToArray();
        var faceValue = EnumHelper.Parse<FaceValue>(splitCard.First());
        var suit = EnumHelper.Parse<Suit>(splitCard.Last());
        return new Card(suit, faceValue);
    }
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