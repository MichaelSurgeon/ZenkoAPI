using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionsToDatabaseAsync(List<Transaction> transactions);
        Task AddFileMetadataToDatabase(FileData fileInfo);
        Task DeleteFileInformationByUserIdAsync(Guid userId);
        Task DeleteTransactionsByUserIdAsync(Guid userId);
    }
}
