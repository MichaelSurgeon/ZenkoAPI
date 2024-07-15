using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface IFileRepository
    {
        Task AddFileMetadataToDatabase(FileData fileInfo);
        Task DeleteFileInformationByUserIdAsync(Guid userId);
        Task<List<FileData>> GetFileInfoAsync(Guid userId);
    }
}
