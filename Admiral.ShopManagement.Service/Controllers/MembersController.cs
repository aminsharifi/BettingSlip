using Admiral.ShopManagement.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Admiral.ShopManagement.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext appDbContext) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<AppUser>>> GetAll()
        {
            return Ok(await appDbContext.Users.ToListAsync());
        }
        [HttpGet("{UniqueId}")]
        public async Task<ActionResult<AppUser>> Get(Guid UniqueId)
        {
            var _User = await appDbContext.Users.FindAsync(UniqueId);
            
            if(_User is null)
                return NotFound("User not found");

            return Ok(_User);
        }
    }
}
