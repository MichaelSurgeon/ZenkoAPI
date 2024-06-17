using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/DataProcessing")]
    public class DataProcessingController : Controller
    {
        // GET: DataProcessingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DataProcessingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DataProcessingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DataProcessingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DataProcessingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DataProcessingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: DataProcessingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DataProcessingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
