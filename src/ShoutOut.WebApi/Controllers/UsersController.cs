using Microsoft.AspNetCore.Mvc;

namespace ShoutOut.WebApi.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("test");
    }
}