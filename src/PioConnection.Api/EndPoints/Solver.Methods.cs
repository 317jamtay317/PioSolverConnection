using Client.Plugins;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PioConnection.Api.Dtos;
using PioConnection.Api.Logging;
using PioConnection.Api.Requests;
using PioConnection.Api.Services;

namespace PioConnection.Api.EndPoints;

public partial class Solver
{
    internal static async Task<IResult> GetFlopRange(
        [FromBody] FlopRangeRequest request,
        [FromServices] IRangeService rangeService,
        [FromServices] ILoggerWrapper<Solver> loggerWrapper)
    {
        try
        {
            var result = rangeService.GetRange(request);
            var apiResult = new ApiResult<string>()
            {
                Data = result,
                IsSuccess = true,
                Errors = null
            };
            return Results.Ok(apiResult);
        }
        catch (JsonSerializationException e)
        {
            loggerWrapper.Error(e.Message);
            var apiResult = new ApiResult<string>()
            {
                Data = null,
                IsSuccess = false,
                Errors =
                [
                    new ErrorInfo()
                    {
                        Message = "Error while parsing",
                    },
                    new ErrorInfo()
                    {
                        Message = e.Message
                    }
                ]
            };
            return Results.BadRequest(apiResult);
        }
        catch (Exception e)
        {
            loggerWrapper.Error(e.Message);
            return Results.BadRequest(
                new ApiResult<string>()
                {
                    Data = null,
                    IsSuccess = false,
                    Errors =
                    [
                        new ErrorInfo()
                        {
                            Message = e.Message
                        }
                    ]
                });
        }
    }
}