using PioConnection.Api.Dtos;

namespace PioConnection.Api.Requests;

public abstract class RangeRequest
{
    public abstract Street Street { get; }
    
    /// <summary>
    /// Gets or sets the path of the cfr file
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Gets or sets what position that you're requesting.
    /// </summary>
    public PlayPosition Position { get; set; }

    /// <summary>
    /// Gets or sets the list of actions that has happened to this point
    /// </summary>
    public IEnumerable<PlayerAction>? Actions { get; set; }
}