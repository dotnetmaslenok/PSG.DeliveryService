using Microsoft.AspNetCore.Mvc;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
public sealed class TestController : ControllerBase
{
    [HttpGet("api/test/getString")]
    public IActionResult Test([FromQuery(Name = "s")] string testString)
    {
        return Ok(testString);
    }
}