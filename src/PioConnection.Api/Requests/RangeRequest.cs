using Newtonsoft.Json;
using PioConnection.Api.Dtos;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public abstract class RangeRequest
{
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
    public PlayPosition Position { get; set; }
}