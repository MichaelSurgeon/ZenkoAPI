using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Models;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController(IUserOperationsService userOperationsService) : Controller
    {
        [HttpPost("createUser")]
        public async Task<ActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var currentUser = await userOperationsService.GetUserBasedOnEmail(user);
            if (currentUser != null)
            {
                return Conflict();
            }

            await userOperationsService.CreateUserAsync(user);

            return Ok();
        }
    }
}
