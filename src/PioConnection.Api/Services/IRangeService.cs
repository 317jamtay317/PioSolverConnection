using PioConnection.Api.Requests;

namespace PioConnection.Api.Services;

public interface IRangeService
{
    /// <summary>
    /// Gets the range from the solver
    /// </summary>
    string[] GetRange(RangeRequest request);
}