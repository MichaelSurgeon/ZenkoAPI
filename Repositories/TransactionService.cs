using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public class TransactionService(ITransactionRepository transactionRepository) : ITransactionService
    {
        public async Task<List<Transaction>> GetTransactionsAsync(Guid userId) 
            => await transactionRepository.GetTransactionsAsync(userId);
        public async Task<List<Transaction>> GetAggregatedTransactionInfoAsync(Guid userId) 
            => await transactionRepository.GetTransactionsAsync(userId);
    }
}
