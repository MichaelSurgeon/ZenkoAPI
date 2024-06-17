using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/AggregatedData")]
    public class AggregatedDataController : Controller
    {
        [HttpGet]
        public ActionResult GetAggregatedData()
        {
            return View();
        }

        [HttpDelete]
        public ActionResult DeleteAggregatedData(int id)
        {
            return Ok();
        }
    }
}
