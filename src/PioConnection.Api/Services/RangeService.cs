using Client.Util;
using PioConnection.Api.Logging;
using PioConnection.Api.Requests;

namespace PioConnection.Api.Services;

public class RangeService(
    ILoggerWrapper<RangeService> logger,
    ISolverConnection connection) : IRangeService
{
    public string GetRange(RangeRequest request)
    {
        throw new NotImplementedException();
        // switch (request.GetType())
        // {
        //     case typeof(FlopRangeRequest):
        //         return string.Empty;
        //     case typeof(TurnRangeRequest):
        //         return string.Empty;
        //     case typeof(RiverRangeRequest):
        //         return string.Empty;
        //     default:
        //         throw new NotSupportedException("We do not support anything other than Flop,Turn,and River range requests");
        // }
    }
}