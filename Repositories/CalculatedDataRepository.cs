using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public class CalculatedDataRepository(DatabaseContext databaseContext) : ICalculatedDataRepository
    {
        public Task<List<Transaction>> GetUserTransactionsById(Guid userId)
        {
            return databaseContext.Transactions.Where(t => t.UserId == userId).ToListAsync();
        }

        public async Task<bool> AddAggregatedDataAsync(AggregatedTransactions aggregatedTransaction)
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
    }
}
