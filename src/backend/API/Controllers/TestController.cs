using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
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
        
        return Ok(message);
    }
}