using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class CalculationService(IAggregatedDataRepository calculatedDataRepository) : ICalculationService
    {
        public async Task<bool> CreateAggregatedDataAsync(Guid userId)
        {
            var transactions = await calculatedDataRepository.GetUserTransactionsById(userId);

            if(transactions.Any())
            {
                var aggregatedTransaction = new AggregatedTransactions()
                {
                    Id = new Guid(),
                    TotalSpend = transactions.Sum(x => x.TransactionAmount),
                    MostCommonCategory = transactions.GroupBy(c => c.CategoryName).OrderByDescending(group => group.Count()).First().Key,
                    TransactionCount = transactions.Count(),
                    UserId = userId
                };

                var result = await calculatedDataRepository.AddAggregatedDataAsync(aggregatedTransaction);
                if (!result)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
