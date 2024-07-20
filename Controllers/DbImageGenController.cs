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
	    var dto = new DtoBuilder(request).CreateDto();
			var lineBuilder = new ForeignKeyLineBuilder(dto);
			dto.ForeignKeys = lineBuilder.BuildLines();
	    var result = new Builder(dto).Build();
	    return Ok(result);
    }
}
