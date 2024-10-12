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
    /// Gets or sets the flop cards.
    /// you can use a string array of card representation for this.
    /// example: ["As","Ac","Ad"] would be the flop AsAcAd  
    /// </summary>
    public Flop? Flop { get; set; }
    
    /// <summary>
    /// Gets or sets the stack size that we are viewing
    /// </summary>
    public StackSize StackSize { get; set; }
    
    /// <summary>
    /// Gets a value to represent the place in the hand we are.
    /// </summary>
    [JsonIgnore]
    public abstract Street Street { get; }
    
    /// <summary>
    /// Gets or sets what oop players position that you're requesting.
    /// </summary>
    public PlayerPosition OOPPlayerPosition { get; set; }
    
    /// <summary>
    /// Gets or sets what ip players position that you're requesting.
    /// </summary>
    public PlayerPosition IPPlayerPosition { get; set; }

    /// <summary>
    /// Builds the node string to represent this request
    /// </summary>
    /// <returns></returns>
    public abstract string BuildNodeString();
}