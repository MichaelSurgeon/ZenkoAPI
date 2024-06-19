using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class UserOperationsService(DatabaseContext databaseContext, IAccountRepository accountRepository) : IUserOperationsService
    {
        public async Task<bool> CreateUserAsync(User user)
        {
            var newUser = new User()
            {
                UserId = new Guid(),
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                Address = user.Address
            };

            return await accountRepository.AddUserToDatabaseAsync(newUser);
        }

        public async Task<User> GetUserAsync(User user) 
        { 
            var currUser = await accountRepository.GetUserAsync(user);
            if (currUser == null) 
            {
                return null;
            }

            return currUser;
        }

        public async Task<bool> DeleteUserAsync(User user) => await accountRepository.DeleteUserFromDatabaseAsync(user);
    }
}
