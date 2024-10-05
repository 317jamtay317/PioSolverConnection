using Microsoft.AspNetCore.Mvc;
using PioConnection.Api.Dtos;

namespace PioConnection.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthCheckController:ControllerBase
{
    /// <summary>
    /// a basic endpoint to check to see of the server is up and running
    /// </summary>
    [HttpGet]
    [Route("check")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public IActionResult Check()
    {
        return Ok(new ApiResponse<string>()
        {
            Data = "Success! We are working",
            IsSuccess = true
        });
    }
}