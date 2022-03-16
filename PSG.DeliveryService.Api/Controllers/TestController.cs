using Microsoft.AspNetCore.Mvc;

namespace PSG.DeliveryService.Api.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    [HttpGet]
    [Route("str")]
    public string GetString()
    {
        return "Success";
    }
}