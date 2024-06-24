using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/aggregatedData")]
    public class AggregatedDataController(IUserOperationsService userOperationsService) : Controller
    {

        [HttpPost("createAnalysis")]
        public ActionResult AnalyseData(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = userOperationsService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            //call calculation service to create aggregated data and populate database table

            return Ok();
        }

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
