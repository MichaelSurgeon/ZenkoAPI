using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/calculations")]
    public class CalculationController(IUserOperationsService userOperationsService, ICalculationService calculationService) : Controller
    {
        [HttpPost("createCalculation")]
        public async Task<ActionResult> AnalyseData(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userOperationsService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            await calculationService.DeleteCalculatedDataAsync(userId);    
            var result = await calculationService.CreateCalculatedData(userId);
            if(!result)  
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
