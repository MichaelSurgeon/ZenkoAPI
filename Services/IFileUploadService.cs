namespace ZenkoAPI.Services
{
    public interface IFileUploadService
    {
        Task AddTransactionToDatabase(Stream file, Guid userId);
        Task AddFileMetaDataToDatabaseAsync(IFormFile file, Guid userId);
        Task DeleteTransactionAndFileInformationAsync(Guid userId);
    }
}
