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
    ILoggerWrapper<RangeController> logger) : ControllerBase
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