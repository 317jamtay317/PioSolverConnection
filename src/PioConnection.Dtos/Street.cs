namespace PioConnection.Dtos;

public enum Street
{
    /// <summary>
    /// The first street of action after the community cards
    /// have been displayed
    /// </summary>
    Flop = 0,
    
    /// <summary>
    /// The second street of action after the community cards
    /// have been displayed
    /// </summary>
    Turn = 1,
    
    /// <summary>
    /// The last street of action after community cards habe
    /// been displayed
    /// </summary>
    River = 2
}