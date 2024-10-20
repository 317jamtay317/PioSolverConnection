using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using PioConnection.Api.Dtos;
using PioConnection.Api.Logging;
using PioConnection.Api.Requests;
using PioConnection.Api.Services;
using PioConnection.Dtos;

namespace PioConnection.Api.Controllers;

[ApiController]
[Route("bet-sizes")]
public class BetSizeController(
    IBetSizingService sizingService,
    ILoggerWrapper<BetSizingService> logger,
    IValidator<FlopSizingRequest> flopValidator) : ControllerBase
{
    /// <summary>
    /// Gets the available sizes from the solver for passed in node on the flop
    /// </summary>
    [Route("flop")]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetFlopSizing(FlopSizingRequest flopSizingRequest)
    {
        var result = flopValidator.Validate(flopSizingRequest);
        if (!result.IsValid)
        {
            return InvalidResponse(result);
        }

        return SingsResult(flopSizingRequest);
    }

    /// <summary>
    /// Gets the available sizes from the solver for passed in node on the turn
    /// </summary>
    [Route("turn")]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetTurnSizing(TurnSizingRequest request)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Gets the available sizes from the solver for passed in node on the river
    /// </summary>
    [Route("river")]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetRiverSizing(RiverSizingRequest riverSizingRequest)
    {
        throw new NotImplementedException();
    }

    private IActionResult SingsResult(FlopSizingRequest request)
    {
        ApiResponse<PlayerAction[]> apiResponse = new();
        try
        {
            var sizings = sizingService.GetSizings(request);
            apiResponse.Data = sizings.ToArray();
            apiResponse.IsSuccess = true;
            return Ok(apiResponse);
        }
        catch (Exception e)
        {
            logger.Error(e.Message);
            apiResponse.IsSuccess=false;
            apiResponse.Errors =
            [
                new ErrorInfo(e.Message)
            ];
            return BadRequest(e.Message);
        }
    }

    private IActionResult InvalidResponse(ValidationResult result)
    {
        var response = new ApiResponse<float[]>()
        {
            IsSuccess = false,
            Errors = result.Errors.Select(x=>new ErrorInfo(x.ToString()))
        };
        result.Errors.ForEach(x=> logger.Warning(x.ToString()));
        return BadRequest(response);
    }
}