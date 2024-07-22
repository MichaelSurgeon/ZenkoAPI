using ZenkoAPI.Models;

namespace ZenkoAPI.Services
{
    public interface IUserOperationsService
    {
        Task<bool> CreateUserAsync(User user);
        Task<User> GetUserAsync(User user);
        Task<bool> DeleteUserAsync(User user);
        Task<bool> UpdateUserAsync(User newUser, User currentUser);
        Task<User> GetUserByIdAsync(Guid userId);
    }
}
