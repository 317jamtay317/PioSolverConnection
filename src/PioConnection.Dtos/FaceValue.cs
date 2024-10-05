using System.ComponentModel;

namespace PioConnection.Dtos;

public enum FaceValue
{
    [Description("A")]
    Ace,
    [Description("K")]
    King,
    [Description("Q")]
    Queen,
    [Description("J")]
    Jack,
    [Description("T")]
    Ten,
    [Description("9")]
    Nine,
    [Description("8")]
    Eight,
    [Description("7")]
    Seven,
    [Description("6")]
    Six,
    [Description("5")]
    Five,
    [Description("4")]
    Four,
    [Description("3")]
    Three,
    [Description("2")]
    Two
}