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
    IConfiguration configuration) : IRangeService
{
    public string[] GetRange(RangeRequest request)
    {
        var solverPath = configuration.GetValue<string>("piosolver-path");
        if (string.IsNullOrWhiteSpace(solverPath))
        {
            throw new ArgumentNullException(nameof(solverPath), "Path is required to start the solver, please ensure that a setting called 'piosolver-path' is in appsettings");
        }

        var metadata = new SolverMetadata(solverPath, request.FilePath);
        using var connection = connectionFactory.Create(metadata);
        LoadTreeCommand loadTreeCommand = new(connection);
        loadTreeCommand.Execute(request.FilePath);
        RangeCommand rangeCommand = new(connection)
        {
            NodeString = request.BuildNodeString()
        };
        if (request is FlopRangeRequest flopRangeRequest)
        {
            rangeCommand.Actions.Add(Street.Flop, flopRangeRequest.FlopActions);
        }

        if (request is TurnRangeRequest turnRangeRequest)
        {
            rangeCommand.Actions.Add(Street.Turn, turnRangeRequest.TurnActions);
        }

        if (request is RiverRangeRequest riverRangeRequest)
        {
            rangeCommand.Actions.Add(Street.River, riverRangeRequest.RiverActions);
        }

        return rangeCommand.Execute().Cast<string>().ToArray();
    }
}