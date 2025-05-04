using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController(ILogger<TestController> logger): ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {  
        var message = new
        {
            message = "Answer from ASP.NET",
            timestamp = DateTime.Now
        };
        
        logger.LogInformation("Responding to GET with message {@Message}", message);
        
        return Ok(message);
    }
}