using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/AggregatedData")]
    public class AggregatedDataController : Controller
    {
        // GET: AggregatedDataController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AggregatedDataController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AggregatedDataController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AggregatedDataController/Create
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

        // GET: AggregatedDataController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AggregatedDataController/Edit/5
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

        // GET: AggregatedDataController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AggregatedDataController/Delete/5
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
