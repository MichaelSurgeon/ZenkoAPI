using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public class CalculationRepository(DatabaseContext databaseContext) : ICalculationRepository
    {
        public Task<List<Transaction>> GetUserTransactionsById(Guid userId) =>
            databaseContext.Transactions.Where(t => t.UserId == userId).ToListAsync();

        public async Task AddCalculatedCategoriesDataAsync(List<CalculatedCategories> aggregatedTransaction)
        {
                await databaseContext.CalculatedCategories.AddRangeAsync(aggregatedTransaction);
                await databaseContext.SaveChangesAsync();
        }

        public async Task<List<CalculatedCategories>> GetCalculatedCategoriesDataAsync(Guid userId) =>
            await databaseContext.CalculatedCategories.Where(x => x.UserId == userId).ToListAsync();

        public async Task DeleteAggregatedCategoriesDataAsync(Guid userId) => 
            await databaseContext.CalculatedCategories.Where(x => x.UserId == userId).ExecuteDeleteAsync();
    }
}
