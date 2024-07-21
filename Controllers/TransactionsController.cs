using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Transactions;
using ZenkoAPI.Dtos;
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
        public async Task<ActionResult<PaginatedTransactionResponse>> GetTransactions(Guid userId, int pageNumber, int pageSize)
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

            var mappedTransactions = transactions.Select(x => new TransactionDto(
                   x.TransactionName,
                   x.TransactionAmount.ToString(),
                   x.TransactionLocation,
                   x.TransactionDate.ToString("g"),
                   x.CategoryName
            )).ToList();

            var pageCount = (int)Math.Ceiling(mappedTransactions.Count / (double)pageSize);
            var paginatedTransactions = mappedTransactions.OrderBy(x => x.Date)
                                                    .Skip((pageNumber - 1) * pageSize)
                                                    .Take(pageSize)
                                                    .ToList();

            var response = new PaginatedTransactionResponse()
            {
                Transactions = paginatedTransactions,
                TotalPages = (int)Math.Ceiling(mappedTransactions.Count / (double)pageSize)
            };

            return response;
        }

        [HttpGet("GetAllTransactionsByDate")]
        public async Task<ActionResult<List<SpendPerDayDto>>> GetAllTransactions(Guid userId)
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

            var spendPerDay = transactions
              .GroupBy(t => t.TransactionDate)
              .Select(group => new SpendPerDayDto
              {
                  Date = group.Key,
                  Amount = group.Sum(t => t.TransactionAmount)
              })
              .OrderBy(x => x.Date)
              .ToList();

            return spendPerDay;
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
