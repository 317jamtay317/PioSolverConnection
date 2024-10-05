namespace PioConnection.Dtos;

public enum ActionType
{
    /// <summary>
    /// Represents a player folding
    /// </summary>
    Fold = 0,
    
    /// <summary>
    /// Represents a player checking
    /// </summary>
    Check = 1,
    
    /// <summary>
    /// Repersents a player betting
    /// </summary>
    Bet = 2,
    
    /// <summary>
    /// Reperesents a player raising
    /// </summary>
    Raise
}