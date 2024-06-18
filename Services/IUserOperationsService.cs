using System.Net;
using ZenkoAPI.Models;

namespace ZenkoAPI.Services
{
    public interface IUserOperationsService
    {
        Task CreateUserAsync(User user);
        Task<User> GetUserBasedOnEmail(User user);
    }
}
