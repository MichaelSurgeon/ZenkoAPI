using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface ICalculationRepository
    {
        Task<List<Transaction>> GetUserTransactionsById(Guid userId);
        Task AddAggregatedTransactionDataAsync(AggregatedTransactions aggregatedTransaction);
        Task<AggregatedTransactions> GetAggregatedTransactionDataAsync(Guid userId);
        Task AddAggregatedCategoriesDataAsync(List<CalculatedCategories> aggregatedTransaction);
        Task<List<CalculatedCategories>> GetAggregatedCategoriesDataAsync(Guid userId);
        Task DeleteAggregatedTransactionDataAsync(Guid userId);
        Task DeleteAggregatedCategoriesDataAsync(Guid userId);
    }
}
