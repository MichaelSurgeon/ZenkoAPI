using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface IAggregatedDataRepository
    {
        Task<List<Transaction>> GetUserTransactionsById(Guid userId);
        Task<bool> AddAggregatedDataAsync(AggregatedTransactions aggregatedTransaction);
    }
}
