using System.ComponentModel;

namespace PioConnection.Dtos;

/// <summary>
/// Enum to represent the suits in the deck
/// </summary>
public enum Suit
{
    [Description("s")]
    Spades,
    
    [Description("h")]
    Hearts,
    
    [Description("d")]
    Diamonds,
    
    [Description("c")]
    Clubs
}