using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Models;
using ZenkoAPI.Repositories;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/transactions")]
    public class TransactionsController(ITransactionRepository transactionRepository, IUserOperationsService userOperationsService) : Controller
    {
        [HttpGet("GetPaginatedTransactions")]
        public async Task<ActionResult<List<Transaction>>> GetTransactions(Guid userId, int pageNumber, int pageSize)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await GetUser(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var transactions = await transactionRepository.GetTransactionsAsync(userId);
            if (transactions.Count == 0)
            {
                return NotFound("No transactions found");
            }

            var paginatedTransactions = transactions.OrderBy(x => x.TransactionDate)
                                            .Skip((pageNumber - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();
                                           
            return paginatedTransactions;
        }

        [HttpGet("GetAggregatedTransactionInfo")]
        public async Task<ActionResult<AggregatedTransaction>> GetAggregatedTransactionInfo(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await GetUser(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var aggregatedTransactionResult = await transactionRepository.GetAggregatedTransactionDataAsync(userId);
            if (aggregatedTransactionResult == null)
            {
                return NotFound("No aggregated transaction found");
            }

            return aggregatedTransactionResult;
        }

        private async Task<User> GetUser(Guid userId) => await userOperationsService.GetUserByIdAsync(userId);
    }
}
