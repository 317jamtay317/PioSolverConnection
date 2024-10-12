using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PioConnection.Api.Core;
using PioConnection.Api.Dtos;
using PioConnection.Api.Logging;
using PioConnection.Api.Requests;
using PioConnection.Api.Services;

namespace PioConnection.Api.Controllers;

[ApiController]
[Route("range")]
public class RangeController(
    IRangeService rangeService, 
    ILoggerWrapper<RangeController> logger,
    IValidator<FlopRangeRequest> flopRequestValidator,
    IValidator<TurnRangeRequest> turnRangeValidator) : ControllerBase
{
    /// <summary>
    /// Gets the flop range from the solver.
    /// </summary>
    /// <param name="request">the request that helps build the tree</param>
    [HttpPost]
    [Route("flop")]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetFlopRange([FromBody] FlopRangeRequest request)
    {
        var validationResult = flopRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                logger.Info(error.ToString());
            }
            return BadRequest(
                new ApiResponse<string>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => new ErrorInfo(x.ErrorMessage))
                });
        }
        return GetRangeResult(request);
    }

    /// <summary>
    /// Gets the turn range from the solver
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("turn")]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetTurnRange([FromBody] TurnRangeRequest request)
    {
        var validationResult = turnRangeValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            foreach (var error in validationResult.Errors)
            {
                logger.Info(error.ToString());
            }
            return BadRequest(
                new ApiResponse<string>()
                {
                    IsSuccess = false,
                    Errors = validationResult.Errors.Select(x => new ErrorInfo(x.ErrorMessage))
                });
        }
        return GetRangeResult(request);
    }

    /// <summary>
    /// Gets the river range from the solver
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("river")]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetRiverRange([FromBody] RiverRangeRequest request)
    {
        return GetRangeResult(request);
    }
    
    private IActionResult GetRangeResult(FlopRangeRequest request)
    {
        ApiResponse<string[]> response = new();
        try
        {
            var range = rangeService.GetRange(request);
            response.IsSuccess = true;
            response.Data = range;
            return Ok(response);
        }
        catch (Exception e)
        {
            logger.Error(e.Message);
            response.IsSuccess = false;
            response.Errors = [new(e.Message)];
            return BadRequest(response);
        }
    }
}