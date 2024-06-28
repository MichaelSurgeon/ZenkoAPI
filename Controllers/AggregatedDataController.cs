using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/aggregatedData")]
    public class AggregatedDataController(IUserOperationsService userOperationsService, ICalculationService calculationService) : Controller
    {
        [HttpPost("create")]
        public async Task<ActionResult> AnalyseData(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userOperationsService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(ModelState);
            }

            var result = await calculationService.CreateAggregatedDataAsync(userId);
            if(!result)  
            {
                return BadRequest("Error occured during calcualtion");
            }
            return Ok();
        }

        [HttpGet]
        public ActionResult GetAggregatedData()
        {
            return View();
        }
    }
}
