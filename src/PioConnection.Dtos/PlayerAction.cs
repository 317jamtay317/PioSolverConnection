using System.Text;

namespace PioConnection.Dtos;

/// <summary>
/// Describes the action that a player takes
/// </summary>
public class PlayerAction
{
    /// <summary>
    /// The action that the played made
    /// </summary>
    public ActionType ActionType { get; set; }

    /// <summary>
    /// Gets or setsThe percentage of the pot that the user bet. See Remarks... 
    /// </summary>
    /// <remarks>
    /// This can be null, if this is null and the <see cref="ActionType"/> is bet or raise we will
    /// throw an exception.
    /// </remarks>
    public int? Size { get; set; }

    public override string ToString()
    {
        return ActionType switch
        {
            ActionType.Fold => "f",
            ActionType.Check or ActionType.Call => "c",
            ActionType.Bet or ActionType.Raise => $"b{Size}",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}