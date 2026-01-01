using Microsoft.AspNetCore.Mvc;

namespace Admiral.ShopManagement.Service.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Vienna Central", City = "Vienna", IsActive = true },
            new { Id = 2, Name = "Graz South", City = "Graz", IsActive = true }
        });
    }
}
