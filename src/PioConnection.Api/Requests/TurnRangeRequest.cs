using PioConnection.Api.Dtos;

namespace PioConnection.Api.Requests;

public class TurnRangeRequest : RangeRequest
{
    public override Street Street => Street.Turn;
}