using PioConnection.Api.Dtos;

namespace PioConnection.Api.Requests;

public class FlopRangeRequest : RangeRequest
{
    public override Street Street => Street.Flop;
}