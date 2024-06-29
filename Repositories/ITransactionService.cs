using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsAsync(Guid userId);
        Task<List<Transaction>> GetAggregatedTransactionInfoAsync(Guid userId);
    }
}
