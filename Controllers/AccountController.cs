using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Services;
using ZenkoAPI.Models;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/account")]
    public class AccountController : Controller
    {
        private readonly IUserOperationsService _userOperationsService;

        public AccountController(IUserOperationsService userOperationsService)
        {
            _userOperationsService = userOperationsService ?? throw new ArgumentNullException(nameof(userOperationsService));
        }

        [HttpPost("createUser")]
        public ActionResult CreateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userOperationsService.CreateUserAsync(user);
            return Ok();
        }

        [HttpGet("getUser")]
        public ActionResult GetUser(IFormCollection collection)
        {

            _userOperationsService.GetUserAsync();
            return Ok();
        }

        [HttpPost("updateUser")]
        public ActionResult UpdateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _userOperationsService.GetUserAsync();
            return Ok();
        }
    }
}
