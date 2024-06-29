using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ZenkoAPI.Models;
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

            var user = await GetUser(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            await calculationService.DeleteCalculatedDataAsync(userId);
           
            var result = await calculationService.CreateCalculatedData(userId);
            if(!result)  
            {
                return BadRequest("Error occured during calcualtion");
            }

            return Ok();
        }

        [HttpGet("getAggregatedData")]
        public async Task<ActionResult<AggregatedTransactions>> GetTransactionAggregatedData(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            var user = await GetUser(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var aggregatedTransactionResult = await calculationService.GetCalculatedTransactionDataAsync(userId);
            if(aggregatedTransactionResult == null)
            {
                return NotFound("No aggregated transactions found");
            }

            return aggregatedTransactionResult;
        }

        [HttpGet("getCalculatedCategories")]
        public async Task<ActionResult<List<CalculatedCategories>>> GetCategoriesAggregatedData(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await GetUser(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var aggregatedCategoriesResult = await calculationService.GetCalculatedCategoriesDataAsync(userId);
            if (aggregatedCategoriesResult.Count == 0)
            {
                return NotFound("No aggregated transactions found");
            }

            return aggregatedCategoriesResult;
        }

        private async Task<User> GetUser(Guid userId) => await userOperationsService.GetUserByIdAsync(userId);

    }
}
