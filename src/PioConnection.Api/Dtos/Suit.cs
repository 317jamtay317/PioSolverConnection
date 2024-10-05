using System.ComponentModel;

namespace PioConnection.Api.Dtos;

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