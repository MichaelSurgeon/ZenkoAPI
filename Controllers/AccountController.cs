using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Controllers.Helpers;
using ZenkoAPI.Models;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController(IUserOperationsService userOperationsService, IPasswordHasher passwordHasher) : Controller
    {
        [HttpPost("createUser")]
        public async Task<ActionResult> CreateUserAsync(User user)
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
        public async Task<ActionResult<User>> GetUserAsync([FromQuery] User user)
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

            if (!passwordHasher.VerifyPassword(retrievedUser.Password, user.Password))
            {
                return Unauthorized();
            }

            return retrievedUser;
        }

        [HttpPatch("updateUser")]
        public async Task<ActionResult> UpdateUserAsync(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var retrievedUser = await userOperationsService.GetUserByIdAsync(user.UserId);
            if (retrievedUser == null)
            {
                return NotFound();
            }

            var result = await userOperationsService.UpdateUserAsync(user, retrievedUser);
            if (!result)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        [HttpPost("deleteUser")]
        public async Task<ActionResult> DeleteUserAsync(User user)
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
