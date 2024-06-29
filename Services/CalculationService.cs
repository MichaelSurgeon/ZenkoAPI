using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class CalculationService(ICalculationRepository calculatedDataRepository, ICachingService cachingService,
        IConfiguration configuration) : ICalculationService
    {
        public async Task<bool> CreateCalculatedData(Guid userId)
        {
            var transactions = await GetTransactionDataAsync(userId);

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

                await calculatedDataRepository.AddAggregatedTransactionDataAsync(aggregatedTransaction);
                await CalculatedCategoriesAsync(transactions, userId);
                return true;
            }
            return false;
        }

        public async Task<AggregatedTransactions> GetCalculatedTransactionDataAsync(Guid userId) =>
            await calculatedDataRepository.GetAggregatedTransactionDataAsync(userId);

        public async Task<List<CalculatedCategories>> GetCalculatedCategoriesDataAsync(Guid userId) =>
            await calculatedDataRepository.GetAggregatedCategoriesDataAsync(userId);

        public async Task DeleteCalculatedDataAsync(Guid userId)
        {
            await calculatedDataRepository.DeleteAggregatedTransactionDataAsync(userId);
            await calculatedDataRepository.DeleteAggregatedCategoriesDataAsync(userId);
        }

        private async Task CalculatedCategoriesAsync(List<Transaction> transactions, Guid userId)
        {
            var groupedTransactions = transactions.GroupBy(x => x.CategoryName);
            var calculatedTransactionData = await GetCalculatedTransactionDataAsync(userId);
            var calculatedCategoriesList = new List<CalculatedCategories>();

            foreach (var group in groupedTransactions)
            {
                var totalAmount = group.Sum(x => x.TransactionAmount);
                var calculatedCategory = new CalculatedCategories()
                {
                    Id = new Guid(),
                    AmountSpent = totalAmount,
                    CategoryName = group.Key,
                    TransactionCount = group.Count(),
                    PercentOfIncome = Math.Round((totalAmount / calculatedTransactionData.TotalSpend) * 100, 2),
                    UserId = userId
                };
                calculatedCategoriesList.Add(calculatedCategory);
            }
            await calculatedDataRepository.AddAggregatedCategoriesDataAsync(calculatedCategoriesList);
        }

        private async Task<List<Transaction>> GetTransactionDataAsync(Guid userId)
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
