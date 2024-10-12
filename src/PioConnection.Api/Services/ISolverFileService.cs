using System.Text.RegularExpressions;
using PioConnection.Dtos;
using PioConnection.Dtos.Extensions;

namespace PioConnection.Api.Services;

public record SolverFilePathMetadata(
    Flop Flop,
    GameType GameType,
    StackSize StackSize,
    PlayerPosition OOPPlayerPosition,
    PlayerPosition IPPlayerPosition);

public interface ISolverFileService
{
    /// <summary>
    /// Gets the path of the file that the solver is to open
    /// </summary>
    /// <param name="metadata">Information about the file</param>
    /// <returns>The path of the file</returns>
    public string GetFilePath(SolverFilePathMetadata metadata);
}

public class SolverFileService(IConfiguration configuration) : ISolverFileService
{
    /// <inheritdoc cref="ISolverFileService.GetFilePath"/>
    public string GetFilePath(SolverFilePathMetadata metadata)
    {
        var (flop, gameType, stackSize, oopPosition, ipPosition) = metadata;
        var filePathNamingConvention = configuration.GetValue<string>("file-path-convention");
        if (string.IsNullOrWhiteSpace(filePathNamingConvention))
        {
            throw new ArgumentNullException(nameof(filePathNamingConvention), "Please add the file naming convention to the enviromment");
        }

        if (stackSize == StackSize.NotDefined)
        {
            throw new ArgumentException("Please define the stack size");
        }

        PlayerPosition[] orderedPositions = [oopPosition, ipPosition];
        orderedPositions = orderedPositions.OrderBy(p => p.PreflopOrder()).ToArray();
        var result = Regex.Replace(filePathNamingConvention, Regex.Escape("{GameType}"), gameType.ToString());
        result = Regex.Replace(result, Regex.Escape("{StackSize}"), stackSize.ToFilePathString());
        result = Regex.Replace(result, Regex.Escape("{Positions}"), $"{orderedPositions.First()} vs {orderedPositions.Last()}");
        result = Path.Combine(result, $"{flop.FistCard}{flop.SecondCard}{flop.ThirdCard}.cfr");
        return result;
    }
}