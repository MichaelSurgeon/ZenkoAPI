using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using ZenkoAPI.Models;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/aggregatedData")]
    public class AggregatedDataController(IUserOperationsService userOperationsService, ICalculationService calculationService) : Controller
    {
        [HttpPost("createAggregatedTransactionData")]
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

            var deletionResult = await calculationService.DeleteCalculatedDataAsync(userId);
            if(!deletionResult)
            {
                return BadRequest("Unable to delete historical data");
            }
            
            var result = await calculationService.CreateAggregatedTransactionDataAsync(userId);
            if(!result)  
            {
                return BadRequest("Error occured during calcualtion");
            }

            return Ok();
        }

        [HttpGet("getAggregatedTransactionData")]
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

            var aggregatedTransactionResult = await calculationService.GetAggregatedTransactionData(userId);
            if(aggregatedTransactionResult == null)
            {
                return NotFound("No aggregated transactions found");
            }

            return aggregatedTransactionResult;
        }

        private async Task<User> GetUser(Guid userId)
        {
            var user = await userOperationsService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }
    }
}
