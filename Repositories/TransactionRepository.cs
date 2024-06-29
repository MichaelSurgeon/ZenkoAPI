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
            try
            {
                var cacheKey = configuration.GetValue<string>("TransactionsCacheKey");
                await databaseContext.AddRangeAsync(transactions);
                await databaseContext.SaveChangesAsync();
                cachingService.ClearCache(cacheKey);
                cachingService.SetCache(cacheKey, transactions);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error adding transaction to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding transaction", ex);
            }
        }

        public async Task AddFileMetadataToDatabase(FileData fileInfo)
        {
            try
            {
                await databaseContext.FileData.AddAsync(fileInfo);
                await databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error adding fileInfo to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding fileInfo", ex);
            }
        }

        public async Task DeleteFileInformationByUserIdAsync(Guid userId)
        {
            try
            {
                await databaseContext.FileData.Where(t => t.UserId == userId).ExecuteDeleteAsync();
                await databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error adding fileInfo to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding fileInfo", ex);
            }
        }

        public async Task DeleteTransactionsByUserIdAsync(Guid userId)
        {
            try
            {
                await databaseContext.Transactions.Where(t => t.UserId == userId).ExecuteDeleteAsync();
                await databaseContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error adding fileInfo to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding fileInfo", ex);
            }
        }
    }
}
