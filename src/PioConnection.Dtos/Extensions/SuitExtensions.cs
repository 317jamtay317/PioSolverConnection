namespace PioConnection.Dtos.Extensions;

public static class SuitExtensions
{
    public static string ToDisplayString(this Suit suit)
    {
        return suit switch
        {
            Suit.Spades => "s",
            Suit.Diamonds => "d",
            Suit.Clubs => "c",
            Suit.Hearts => "h",
            _ => throw new ArgumentOutOfRangeException(nameof(suit), suit, null)
        };
    }
}