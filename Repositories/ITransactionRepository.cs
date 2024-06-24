using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface ITransactionRepository
    {
        Task AddTransactionToDatabaseAsync(Transaction transaction);
        Task AddFileMetadataToDatabase(FileData fileInfo);
        Task DeleteFileInformationByUserIdAsync(Guid userId);
        Task DeleteTransactionsByUserIdAsync(Guid userId);
    }
}
