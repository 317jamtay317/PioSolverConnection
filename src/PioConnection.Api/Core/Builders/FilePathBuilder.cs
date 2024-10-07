using PioConnection.Dtos;

namespace PioConnection.Api.Core.Builders;

/// <summary>
/// A class to help build the path of the file
/// </summary>
public class FilePathBuilder()
{
    public FilePathBuilder IsTournament()
    {
        _isTournament = true;
        return this;
    }

    public FilePathBuilder IsCash()
    {
        _isCash = true;
        return this;
    }

    public FilePathBuilder WithStackDepth(int depth)
    {
        _stackDepth = depth;
        return this;
    }

    public FilePathBuilder PotType(PotType potType)
    {
        _potType = potType;
        return this;
    }

    public FilePathBuilder WithPositions(PlayerPosition oopPlayerPosition, PlayerPosition ipPlayerPosition)
    {
        _oopPlayerPosition = oopPlayerPosition;
        _ipPlayerPosition = ipPlayerPosition;
        return this;
    }

    public FilePathBuilder WithFlop(Card card1, Card card2, Card card3)
    {
        _flop.AddRange([card1,card2,card3]);
        return this;
    }
    public override string ToString()
    {
        List<string> pathList = new(){@"F:","sims","ChipEV"};
        if ((!_isCash && !_isTournament) ||
            (_isCash && _isTournament))
        {
            throw new ArgumentException($"You must set either {nameof(IsCash)} or {nameof(IsTournament)}");
        }

        if (_stackDepth == 0)
        {
            throw new ArgumentException($"You must set {nameof(WithStackDepth)}");
        }

        if (_potType == PioConnection.Dtos.PotType.None)
        {
            throw new ArgumentException($"You must set {nameof(PotType)}");
        }

        if (_oopPlayerPosition == _ipPlayerPosition ||
            _oopPlayerPosition > _ipPlayerPosition)
        {
            throw new NotSupportedException("You cannot have the same position for both players, the ip must be a later position than the oop");
        }
        
        if(_isCash)pathList.Add("cash");
        if(_isTournament)pathList.Add("tournaments");
        pathList.Add(_stackDepth.ToString());
        pathList.Add($"{_oopPlayerPosition} vs {_ipPlayerPosition}");
        pathList.Add($"{_flop.FistCard}{_flop.SecondCard}{_flop.ThirdCard}.cfr");

        return Path.Combine(pathList.ToArray());
    }

    private bool _isTournament;
    private bool _isCash;
    private int _stackDepth;
    private PotType _potType;
    private PlayerPosition _oopPlayerPosition;
    private PlayerPosition _ipPlayerPosition;
    private Flop _flop = new();
}