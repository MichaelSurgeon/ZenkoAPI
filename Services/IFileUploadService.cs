namespace ZenkoAPI.Services
{
    public interface IFileUploadService
    {
        Task ParseAndAddTransactionToDatabase(Stream file, Guid userId);
        Task AddFileMetaDataToDatabaseAsync(IFormFile file, Guid userId);
        Task DeleteTransactionsByIdAsync(Guid userId);
    }
}
