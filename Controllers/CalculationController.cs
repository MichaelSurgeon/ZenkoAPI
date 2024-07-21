using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using ZenkoAPI.Dtos;
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

        [HttpGet("getCalculatedCategories")]
        public async Task<ActionResult<List<CalculatedCategoriesResponse>>> GetCalculatedCategories(Guid userId)
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

            var result = await calculationService.GetCalculatedCategoriesDataAsync(userId);
            if(!result.Any())
            {
                return NotFound();
            }

            var response = result.Select(x => new CalculatedCategoriesResponse()
            {
                Category = x.CategoryName,
                AmountSpent = x.AmountSpent,
                TransactionCount = x.TransactionCount,
                PercentOfIncome = x.PercentOfIncome,
                Status = "Test"
            }).ToList();

            return response;
        }
    }
}
