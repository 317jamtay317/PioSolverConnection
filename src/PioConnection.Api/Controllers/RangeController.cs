using Microsoft.AspNetCore.Mvc;
using PioConnection.Api.Requests;

namespace PioConnection.Api.Controllers;

[ApiController]
[Route("range")]
public class RangeController : ControllerBase
{
    [HttpPost]
    public IActionResult GetFlopRange([FromBody] FlopRangeRequest request)
    {
        throw new NotImplementedException();
    }
}