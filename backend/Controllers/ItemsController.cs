using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
  [HttpGet]
  public IActionResult Get() => Ok(new[] {
    new { id = 1, name = "Alpha" },
    new { id = 2, name = "Beta"  },
    new { id = 3, name = "Gamma" }
  });
}

