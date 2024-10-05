using Microsoft.AspNetCore.Mvc;

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
    public IActionResult Check()
    {
        return Ok();
    }
}