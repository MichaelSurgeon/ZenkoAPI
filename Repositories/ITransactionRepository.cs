﻿using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionsToDatabaseAsync(List<Transaction> transactions);
        Task DeleteTransactionsByUserIdAsync(Guid userId);
        Task DeleteAggregatedTransactionDataAsync(Guid userId);
        Task AddAggregatedTransactionDataAsync(AggregatedTransaction aggregatedTransaction);
        Task<List<Transaction>> GetTransactionsAsync(Guid userId);
        Task<AggregatedTransaction> GetAggregatedTransactionDataAsync(Guid userId);
    }
}
