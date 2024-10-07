using Newtonsoft.Json;
using PioConnection.Api.Dtos;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public abstract class RangeRequest
{
    /// <summary>
    /// Gets or sets the type of game we're studying
    /// </summary>
    public GameType GameType { get; set; }
    
    /// <summary>
    /// Gets a value to represent the place in the hand we are.
    /// </summary>
    [JsonIgnore]
    public abstract Street Street { get; }
    
    /// <summary>
    /// Gets or sets the path of the cfr file
    /// </summary>
    public string? FilePath { get; set; }

    /// <summary>
    /// Gets or sets what position that you're requesting.
    /// </summary>
    public PlayerPosition Position { get; set; }

    /// <summary>
    /// Builds the node string to represent this request
    /// </summary>
    /// <returns></returns>
    public abstract string BuildNodeString();
}