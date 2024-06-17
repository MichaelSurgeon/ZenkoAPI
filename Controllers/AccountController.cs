using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/Account")]
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult GetUserCredentials(string email, string password)
        {
            // get user credentials

            return Ok();
        }

        [HttpPost]
        public ActionResult CreateUser(IFormCollection collection)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(collection);
            }

            //create user in database
            return Ok();
        }

        public ActionResult UpdateUser(int id)
        {
            //update user information
            return Ok();
        }

        public ActionResult DeleteUser(int id)
        {
            // delete user from database
            return Ok()        }
    }
}
