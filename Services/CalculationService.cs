using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class CalculationService(IAggregatedDataRepository calculatedDataRepository, ICachingService cachingService,
        IConfiguration configuration) : ICalculationService
    {
        public async Task<bool> CreateCalculatedData(Guid userId)
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

                await CalculatedCategoriesAsync(transactions, userId);
                return true;
            }
            return false;
        }

        public async Task<AggregatedTransactions> GetCalculatedData(Guid userId)
        {
            var result = await calculatedDataRepository.GetAggregatedTransactionDataAsync(userId);
            if (result == null)
            {
                return null;
            }
            return result; ;
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

        private async Task CalculatedCategoriesAsync(List<Transaction> transactions, Guid userId)
        {
            var groupedTransactions = transactions.GroupBy(x => x.CategoryName);
            var calculatedTransactionData = await GetCalculatedData(userId);
            var amountSpent = 0.0m;

            foreach (var group in groupedTransactions)
            {
                foreach(var transaction in group)
                {
                    amountSpent = decimal.Add(amountSpent, transaction.TransactionAmount);
                }

                await calculatedDataRepository.AddAggregatedCalculationTransactionDataAsync(new CalculatedCategories()
                {
                    Id = new Guid(),
                    AmountSpent = amountSpent,
                    CategoryName = group.Key,
                    TransactionCount = group.Count(),
                    PercentOfIncome = Math.Round((amountSpent / calculatedTransactionData.TotalSpend) * 100, 2)
                });
                amountSpent = 0.0m;
            }
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
