using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public class FileRepository(DatabaseContext databaseContext) : IFileRepository
    {
        public async Task AddFileMetadataToDatabase(FileData fileInfo)
        {
            await databaseContext.FileData.AddAsync(fileInfo);
            await databaseContext.SaveChangesAsync();
        }

        public async Task DeleteFileInformationByUserIdAsync(Guid userId)
        {
            await databaseContext.FileData.Where(t => t.UserId == userId).ExecuteDeleteAsync();
            await databaseContext.SaveChangesAsync();
        }
    }
}
