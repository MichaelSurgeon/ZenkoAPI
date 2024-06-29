using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;
using ZenkoAPI.Services;

namespace ZenkoAPI.Repositories
{
    public class TransactionRepository(DatabaseContext databaseContext, ICachingService cachingService, IConfiguration configuration) : ITransactionRepository
    {
        public async Task AddTransactionsToDatabaseAsync(List<Transaction> transactions)
        {
            var cacheKey = configuration.GetValue<string>("TransactionsCacheKey");
            await databaseContext.AddRangeAsync(transactions);
            await databaseContext.SaveChangesAsync();
            cachingService.ClearCache(cacheKey);
            cachingService.SetCache(cacheKey, transactions);
        }

        public async Task AddFileMetadataToDatabase(FileData fileInfo)
        {
            await databaseContext.FileData.AddAsync(fileInfo);
            await databaseContext.SaveChangesAsync();
        }

        public async Task AddAggregatedTransactionDataAsync(AggregatedTransaction aggregatedTransaction)
        {
            await databaseContext.AggregatedTransactions.AddAsync(aggregatedTransaction);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteFileInformationByUserIdAsync(Guid userId)
        {
            await databaseContext.FileData.Where(t => t.UserId == userId).ExecuteDeleteAsync();
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteTransactionsByUserIdAsync(Guid userId) =>
                await databaseContext.Transactions.Where(t => t.UserId == userId).ExecuteDeleteAsync();

        public async Task DeleteAggregatedTransactionDataAsync(Guid userId) =>
                await databaseContext.AggregatedTransactions.Where(x => x.UserId == userId).ExecuteDeleteAsync();

        public async Task<List<Transaction>> GetTransactionsAsync(Guid userId) 
            => await databaseContext.Transactions.Where(x => x.UserId == userId).ToListAsync();

        public async Task<AggregatedTransaction> GetAggregatedTransactionDataAsync(Guid userId) 
            => await databaseContext.AggregatedTransactions.Where(x => x.UserId == userId).FirstAsync();

    }
}
