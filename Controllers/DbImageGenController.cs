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

    [HttpPost("dbimage")]
    public IActionResult GenerateImage(DbImageGenRequest request)
    {
	    var temp = new List<string>();
	    foreach (var t in request.Table)
	    {
		    temp.Add(t.tableName);
	    }
	    return Ok(temp);
    }
}
