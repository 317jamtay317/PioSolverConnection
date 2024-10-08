using Microsoft.AspNetCore.Mvc;
using PioConnection.Api.Dtos;

namespace PioConnection.Api.Controllers;

[ApiController]
[Route("bet-sizes")]
public class BetSizeController : ControllerBase
{
    /// <summary>
    /// Gets the available sizes from the solver for passed in node on the flop
    /// </summary>
    [Route("flop")]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetFlopSizing()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Gets the available sizes from the solver for passed in node on the turn
    /// </summary>
    [Route("turn")]
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<string[]>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status400BadRequest)]
    public IActionResult GetTurnSizing()
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
    public IActionResult GetRiverSizing()
    {
        throw new NotImplementedException();
    }
}