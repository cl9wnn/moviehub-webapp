using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController: ControllerBase
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