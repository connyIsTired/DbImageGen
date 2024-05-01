using Microsoft.AspNetCore.Mvc;
using DbImageGen.Models;

namespace DbImageGen.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DbImageGenController : ControllerBase
{
    private readonly ILogger<DbImageGenController> _logger;

    public DbImageGenController(ILogger<DbImageGenController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "dbimage")]
    public IActionResult Get()
    {
	    var obj = new SvgOutput();
	    return Ok(obj);
    }
}
