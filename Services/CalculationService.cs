using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class CalculationService(IAggregatedDataRepository calculatedDataRepository, ICachingService cachingService,
        IConfiguration configuration) : ICalculationService
    {
        public async Task<bool> CreateAggregatedTransactionDataAsync(Guid userId)
        {
            var transactions = await GetTransactionData(userId);

            if (transactions.Count > 0)
            {
                var aggregatedTransaction = new AggregatedTransactions()
                {
                    Id = new Guid(),
                    TotalSpend = transactions.Sum(x => x.TransactionAmount),
                    MostCommonCategory = transactions.GroupBy(c => c.CategoryName).OrderByDescending(group => group.Count()).First().Key,
                    TransactionCount = transactions.Count,
                    UserId = userId
                };

                var result = await calculatedDataRepository.AddAggregatedTransactionDataAsync(aggregatedTransaction);
                if (!result)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public async Task<AggregatedTransactions> GetAggregatedTransactionData(Guid userId)
        {
            var result = await calculatedDataRepository.GetAggregatedTransactionDataAsync(userId);
            if (result == null)
            {
                return null;
            }
            return result; ;
        }

        public async Task<bool> CreateCalculationAggregatedDataAsync(Guid userId)
        {
            var transactions = await GetTransactionData(userId);

            if(transactions.Count > 0)
            {

            }

            return false;
        }

        public async Task<bool> DeleteCalculatedDataAsync(Guid userId)
        {
            var result = await calculatedDataRepository.DeleteAggregatedTransactionDataAsync(userId);
            if (result)
            {
                return true;
            }
            return false;
        }

        private async Task<List<Transaction>> GetTransactionData(Guid userId)
        {
            var cacheKey = configuration.GetValue<string>("TransactionsCacheKey") ?? "";
            var transactions = cachingService.GetCache<Transaction>(cacheKey);

            if (transactions.Count == 0)
            {
                transactions = await calculatedDataRepository.GetUserTransactionsById(userId);
                cachingService.SetCache(cacheKey, transactions);
                return transactions;
            }

            return transactions;
        }
    }
}
