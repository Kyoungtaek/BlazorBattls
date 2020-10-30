using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattles.Server.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattles.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;

        public UserController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet("GetBananas")]
        public async Task<IActionResult> GetBananas()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            return Ok(user.Bananas);
        }
    }
}
