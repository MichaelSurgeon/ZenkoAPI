using ZenkoAPI.Controllers.Helpers;
using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class UserOperationsService(IAccountRepository accountRepository, IPasswordHasher passwordHasher) : IUserOperationsService
    {
        public async Task<bool> CreateUserAsync(User user)
        {
            var newUser = new User()
            {
                UserId = new Guid(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = passwordHasher.HashPassword(user.Password),
                Address = user.Address
            };

            return await accountRepository.AddUserToDatabaseAsync(newUser);
        }

        public async Task<User> GetUserAsync(User user) => await accountRepository.GetUserAsync(user);
        public async Task<User> GetUserByIdAsync(Guid userId) => await accountRepository.GetUserByIdAsync(userId);
        public async Task<bool> DeleteUserAsync(User user) => await accountRepository.DeleteUserFromDatabaseAsync(user);
    }
}
