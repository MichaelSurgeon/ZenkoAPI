using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public class AggregatedDataRepository(DatabaseContext databaseContext) : IAggregatedDataRepository
    {
        public Task<List<Transaction>> GetUserTransactionsById(Guid userId)
        {
            return databaseContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddAggregatedTransactionDataAsync(AggregatedTransactions aggregatedTransaction)
        {
            try
            {
                await databaseContext.AggregatedTransactions.AddAsync(aggregatedTransaction);
                await databaseContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<AggregatedTransactions> GetAggregatedTransactionDataAsync(Guid userId)
        {
            try
            {
                var aggregatedTransaction = await databaseContext.AggregatedTransactions.Where(x => x.UserId == userId).FirstAsync();
                return aggregatedTransaction;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> DeleteAggregatedTransactionDataAsync(Guid userId)
        {
            try
            { 
                await databaseContext.AggregatedTransactions.Where(x => x.UserId == userId).ExecuteDeleteAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
