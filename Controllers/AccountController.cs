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

            var currentUser = await userOperationsService.GetUserAsync(user);
            if (currentUser != null)
            {
                return Conflict();
            }

            var result = await userOperationsService.CreateUserAsync(user);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpGet("getUser")]
        public async Task<ActionResult<User>> GetUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var retrievedUser = await userOperationsService.GetUserAsync(user);
            if (retrievedUser == null)
            {
                return NotFound();
            }

            if (retrievedUser.Password != user.Password)
            {
                return Unauthorized();
            }

            return retrievedUser;
        }

        [HttpPost("deleteUser")]
        public async Task<ActionResult> DeleteUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var retrievedUser = await userOperationsService.GetUserAsync(user);
            if (retrievedUser == null)
            {
                return NotFound();
            }

            var result = await userOperationsService.DeleteUserAsync(retrievedUser);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }
    }
}
