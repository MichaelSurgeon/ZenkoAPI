using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/DataProcessing")]
    public class DataProcessingController : Controller
    {

        [HttpPost]
        public ActionResult CreateCalculation(IFormCollection collection)
        {
            return Ok();
        }
    }
}
