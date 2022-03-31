using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Authorize(Policy = "Client")]
    public IActionResult GetString([FromQuery(Name = "s")] string content)
    {
        return Ok(content);
    }
}