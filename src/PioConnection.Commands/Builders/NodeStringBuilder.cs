using System.Text;
using PioConnection.Dtos;

namespace PioConnection.Commands.Builders;

public class NodeStringBuilder
{
    /// <summary>
    /// Adds an action to the oop players actions in the node string on the flop.
    /// </summary>
    /// <param name="action">The action</param>
    public NodeStringBuilder WithOOPFlopAction(PlayerAction action)
    {
        _oopFlopActions.Add(action);
        return this;
    }

    /// <summary>
    /// Adds an action to the IP players actions in the node string on the flop. SeeRemarks...
    /// </summary>
    /// <remarks>If This is not added we will be reciving the IP players range</remarks>
    /// <param name="action">The action</param>
    public NodeStringBuilder WithIPFlopAction(PlayerAction action)
    {
        _ipFlopActions.Add(action);
        return this;
    }

    /// <summary>
    /// Adds a turn card to the end string
    /// </summary>
    public NodeStringBuilder WithTurnCard(Card turn)
    {
        _turn = turn;
        return this;
    }

    public NodeStringBuilder WithRiverCard(Card river)
    {
        _river = river;
        return this;
    }

    /// <summary>
    /// Adds a turn action to the oop players action
    /// </summary>
    /// <param name="action">The oop players action</param>
    public NodeStringBuilder WithOOPTurnAction(PlayerAction action)
    {
        _oopTurnActions.Add(action);
        return this;
    }
    /// <summary>
    /// Adds a turn action to the IP players action
    /// </summary>
    /// <param name="action">The IP players action</param>
    public NodeStringBuilder WithIPTurnAction(PlayerAction action)
    {
        _ipTurnActions.Add(action);
        return this;
    }
    
    public override string ToString()
    {
        var stringBuilder = new StringBuilder("r:0");
        BuildFlopActionString(stringBuilder);
        VerifyActions(_ipFlopActions, _oopFlopActions, _turn);
        if (!_turn.HasValue)
        {
            return stringBuilder.ToString();
        } 
        BuildTurnActionString(stringBuilder);
        VerifyActions(_ipTurnActions, _oopFlopActions, _river);
        if (!_river.HasValue)
        {
            return stringBuilder.ToString();
        }
        BuildRiverActions(stringBuilder);
        return stringBuilder.ToString();
    }

    private void BuildRiverActions(StringBuilder stringBuilder)
    {
        stringBuilder.Append($":{_river}");
    }

    private void VerifyActions(List<PlayerAction> ipActions, List<PlayerAction> oopActions, Card? card)
    {
        
        if ((!ipActions.Any() ||!ipActions.Any() && !oopActions.Any() ) 
            && !card.HasValue)
        {
            return;
        }
        var oopLastAction = oopActions.Last().ActionType;
        var ipLastAction = ipActions.Last().ActionType;
        if (oopLastAction == ActionType.Call ||
            ipLastAction == ActionType.Call ||
            (oopLastAction == ActionType.Check && ipLastAction == ActionType.Check))
        {
            if (card.HasValue) return;
                throw new InvalidOperationException("The last action must be a call and the next card must exist");
        }
    }

    private void BuildTurnActionString(StringBuilder stringBuilder)
    {
        stringBuilder.Append($":{_turn}");
        for (int i = 0; i < _oopTurnActions.Count; i++)
        {
            var oopTurnAction = _oopTurnActions[i];
            stringBuilder.Append($":{oopTurnAction}");
            if (_ipTurnActions.Count - 1 >= i)
            {
                var ipTurnAction = _ipTurnActions[i];
                stringBuilder.Append($":{ipTurnAction}");
            }
        }
    }

    private void BuildFlopActionString(StringBuilder stringBuilder)
    {
        for (int i = 0; i < _oopFlopActions.Count; i++)
        {
            var oopAction = _oopFlopActions[i];
            stringBuilder.Append($":{oopAction}");
            if (_ipFlopActions.Count - 1 >= i)
            {
                var ipAction = _ipFlopActions[i];
                stringBuilder.Append($":{ipAction}");
            }
        }
    }

    private readonly List<PlayerAction> _oopFlopActions = new();
    private readonly List<PlayerAction> _ipFlopActions = new();
    private readonly List<PlayerAction> _oopTurnActions = new();
    private readonly List<PlayerAction> _ipTurnActions = new();
    private readonly List<PlayerAction> _oopRiverActions = new();
    private readonly List<PlayerAction> _ipRiverActions = new();
    private Card? _turn = null;
    private Card? _river = null;
}