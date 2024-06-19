using Microsoft.AspNetCore.Mvc;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/DataProcessing")]
    public class DataProcessingController : Controller
    {
        [HttpPost]
        public ActionResult FileUpload(IFormFile file)
        {
       

            return Ok();
        }
    }
}
