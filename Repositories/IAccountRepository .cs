using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> AddUserToDatabaseAsync(User user);
        Task<bool> DeleteUserFromDatabaseAsync(User user);
        Task<User> GetUserAsync(User user);
        Task<bool> UpdateUserAsync(User user);
        Task<User> GetUserByIdAsync(Guid userId);
    }
}
