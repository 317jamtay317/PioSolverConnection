using Client.Plugins;
using Microsoft.AspNetCore.Connections;
using Microsoft.Extensions.Configuration;
using PioConnection.Api.Core;
using PioConnection.Api.Logging;
using PioConnection.Api.Requests;
using PioConnection.Commands;
using PioConnection.Dtos;

namespace PioConnection.Api.Services;

public interface IBetSizingService
{
    IEnumerable<PlayerAction> GetSizings(FlopSizingRequest request);
}

public class BetSizingService(ISolverFileService fileService,
    IConfiguration configuration,
    ILoggerWrapper<BetSizingService> loggerWrapper,
    ISolverConnectionFactory connectionFactory) : IBetSizingService
{
    public IEnumerable<PlayerAction> GetSizings(FlopSizingRequest request)
    {
        var solverPath = configuration.GetValue<string>("piosolver-path");
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
        SizingCommand sizingCommand = new(connection)
        {
            NodeString = request.BuildNodeString()
        };
        var solverResponse= sizingCommand.Execute();
        var actions = solverResponse
            .OfType<string>()
            .Where(x=>x.StartsWith("r:0"))
            .Select(ParseResponse)
            .ToArray();
        return actions;
    }

    private PlayerAction ParseResponse(string arg)
    {
        var splitActions = arg.Split(":");
        return splitActions;
    }
}