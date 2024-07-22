using Microsoft.AspNetCore.Mvc;
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
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("getCalculatedCategories")]
        public async Task<ActionResult<List<CalculatedCategoriesResponse>>> GetCalculatedCategoriesAsync(Guid userId)
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
            if (!result.Any())
            {
                return NotFound();
            }

            var response = result.Select(x => new CalculatedCategoriesResponse()
            {
                Category = x.CategoryName,
                AmountSpent = x.AmountSpent,
                TransactionCount = x.TransactionCount,
                PercentOfIncome = x.PercentOfIncome,
                Status = GetStatus(x.PercentOfIncome),
            }).ToList();

            return response;
        }

        [HttpGet("getBudgetSplit")]
        public async Task<ActionResult<BudgetSplitResponse>> GetBudgetSplitAsync(Guid userId)
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
            if (!result.Any())
            {
                return NotFound();
            }

            var needs = result.Where(x => x.CategoryName == "Bills" || x.CategoryName == "Transport" || x.CategoryName == "Groceries").Sum(x => x.PercentOfIncome);
            var wants = result.Where(x =>
                                         x.CategoryName == "Entertainment" ||
                                         x.CategoryName == "Subscriptions" ||
                                         x.CategoryName == "General" ||
                                         x.CategoryName == "Eating Out" ||
                                         x.CategoryName == "Shopping").Sum(x => x.PercentOfIncome);
            var savingsAndDebts = result.Where(x => x.CategoryName == "Debt").Sum(x => x.PercentOfIncome);

            var response = new BudgetSplitResponse()
            {
                Needs = needs,
                Wants = wants,
                DebtsAndSavings = savingsAndDebts
            };

            return response;
        }

        private static string GetStatus(decimal number)
        {
            if (number < 15)
                return "Great";

            if (number > 15 && number < 25)
                return "Could be better";

            if (number > 25)
                return "Needs work";

            return "N/A";
        }
    }
}
