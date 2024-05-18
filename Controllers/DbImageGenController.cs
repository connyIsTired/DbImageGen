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
	    
	    var result = new Builder().Build(request.Table.First().tableName);
	    return Ok(result);
    }
}
