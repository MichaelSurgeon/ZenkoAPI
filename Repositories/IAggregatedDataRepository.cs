using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface IAggregatedDataRepository
    {
        Task<List<Transaction>> GetUserTransactionsById(Guid userId);
        Task<bool> AddAggregatedTransactionDataAsync(AggregatedTransactions aggregatedTransaction);
        Task<AggregatedTransactions> GetAggregatedTransactionDataAsync(Guid userId);
        Task<bool> AddAggregatedCalculationTransactionDataAsync(CalculatedCategories aggregatedTransaction);
        Task<bool> DeleteAggregatedTransactionDataAsync(Guid userId);
    }
}
