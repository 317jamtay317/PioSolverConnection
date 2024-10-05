using Client.Plugins;
using PioConnection.Api.Dtos;

namespace PioConnection.Api.EndPoints;

public partial class Solver
{
    internal static async Task<IResult> GetRange()
    {
        var rangeMetadata = new RangeMetadata(
            SolverPath:@"C:\PioSOLVER\PioSOLVER2-pro.exe",
            TreePath:@"F:\Sims\BB_Covers_LJ\7h6d5c.cfr");
        var rangePrinter = new RangesPrinter(rangeMetadata);
        var bothRanges = rangePrinter.Run();
        return Results.Ok(new ApiResult<string>()
        {
            IsSuccess = true,
            Data = bothRanges.IPRange,
            Errors = null
        });
    }
}