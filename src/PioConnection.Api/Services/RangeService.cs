using Client.Plugins;
using Client.Util;
using PioConnection.Api.Core;
using PioConnection.Api.Logging;
using PioConnection.Api.Requests;
using PioConnection.Commands;
using PioConnection.Dtos;

namespace PioConnection.Api.Services;

public class RangeService(
    ISolverConnectionFactory connectionFactory,
    IConfiguration configuration,
    ISolverFileService fileService,
    ILoggerWrapper<RangeService> loggerWrapper) : IRangeService
{
    public string[] GetRange(FlopRangeRequest request)
    {
        var solverPath = configuration.GetValue<string>("piosolver-path");
        var files = new FileInfo(solverPath);
        if (string.IsNullOrWhiteSpace(solverPath))
        {
            throw new ArgumentNullException(nameof(solverPath), "Path is required to start the solver, please ensure that a setting called 'piosolver-path' is in appsettings");
        }
        loggerWrapper.Info($"The solver path is {solverPath}");
        var fileMetaData = new SolverFilePathMetadata(
            request.Flop,
            request.GameType,
            request.StackSize,
            request.OOPPlayerPosition,
            request.IPPlayerPosition);
        var filePath = fileService.GetFilePath(fileMetaData);
        loggerWrapper.Debug($"The File path is {filePath}");
        var metadata = new SolverMetadata(solverPath, filePath);
        using var connection = connectionFactory.Create(metadata);
        LoadTreeCommand loadTreeCommand = new(connection);
        loadTreeCommand.Execute($@"""{filePath}""");
        RangeCommand rangeCommand = new(connection)
        {
            NodeString = request.BuildNodeString()
        };

        return rangeCommand.Execute().Cast<string>().ToArray();
    }
}