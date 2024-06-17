using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/Account")]
    public class AccountController : Controller
    {
        // GET: AccountController
        public ActionResult GetUserCredentials()
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        public ActionResult CreateUser(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult UpdateUser(int id)
        {
            return View();
        }

        // GET: AccountController/Delete/5
        public ActionResult DeleteUser(int id)
        {
            return View();
        }
    }
}
