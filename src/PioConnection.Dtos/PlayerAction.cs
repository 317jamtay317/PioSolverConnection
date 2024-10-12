using System.Text;

namespace PioConnection.Dtos;

/// <summary>
/// Describes the action that a player takes
/// </summary>
public class PlayerAction : IEquatable<PlayerAction>
{
    public static bool operator ==(PlayerAction left, PlayerAction right)
    {
        return left?.Equals(right) ?? right is null;
    }

    public static bool operator !=(PlayerAction left, PlayerAction right)
    {
        return !left?.Equals(right) ?? right is not null;
    }
    /// <summary>
    /// Creates a check action
    /// </summary>
    public static PlayerAction Check() => new PlayerAction() { ActionType = ActionType.Check };
    /// <summary>
    /// Creates a call action
    /// </summary>
    public static PlayerAction Call() => new PlayerAction() { ActionType = ActionType.Call };
    /// <summary>
    /// Creates a bet action
    /// </summary>
    public static PlayerAction Bet(int size) => new PlayerAction() { ActionType = ActionType.Bet, Size = size};
    
    /// <summary>
    /// creates a raise action
    /// </summary>
    public static PlayerAction Raise(int size) => new PlayerAction() { ActionType = ActionType.Raise, Size = size};
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

    public bool Equals(PlayerAction? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return ActionType == other.ActionType && Size == other.Size;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != this.GetType())
        {
            return false;
        }

        return Equals((PlayerAction)obj);
    }

    public override int GetHashCode() =>
        HashCode.Combine((int)ActionType, Size);
}