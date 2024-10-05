namespace PioConnection.Dtos;

/// <summary>
/// Describes the action that a player takes
/// </summary>
public class PlayerAction
{
    public ActionType ActionType { get; set; }

    public string? Sizing { get; set; }
}