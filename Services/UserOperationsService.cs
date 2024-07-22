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
        public async Task<bool> UpdateUserAsync(User newUser, User currentUser)
        {
            var updatedUser = new User()
            {
                UserId = currentUser.UserId,
                Email = newUser.Email ?? currentUser.Email,
                FirstName = newUser.FirstName ?? currentUser.FirstName,
                LastName = newUser.LastName ?? currentUser.LastName,
                Address = newUser.Address ?? currentUser.Address,
                Password = passwordHasher.HashPassword(newUser.Password) ??passwordHasher.HashPassword(currentUser.Password)
            };

            return await accountRepository.UpdateUserAsync(updatedUser);
        }
        public async Task<bool> DeleteUserAsync(User user) => await accountRepository.DeleteUserFromDatabaseAsync(user);
    }
}
