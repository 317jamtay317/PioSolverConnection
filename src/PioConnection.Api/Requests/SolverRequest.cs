using Newtonsoft.Json;
using PioConnection.Api.Dtos;
using PioConnection.Dtos;

namespace PioConnection.Api.Requests;

public abstract class SolverRequest
{
    /// <summary>
    /// Gets or sets the type of game we're studying
    /// </summary>
    public GameType GameType { get; set; }
    
    /// <summary>
    /// Gets the file path to be openend
    /// </summary>
    [JsonIgnore]
    public abstract string? FilePath { get; } 
    
    /// <summary>
    /// Gets a value to represent the place in the hand we are.
    /// </summary>
    [JsonIgnore]
    public abstract Street Street { get; }
    
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