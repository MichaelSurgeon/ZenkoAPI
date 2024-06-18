using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;
using ZenkoAPI.Repositories;

namespace ZenkoAPI.Services
{
    public class UserOperationsService(DatabaseContext databaseContext, IAccountRepository accountRepository) : IUserOperationsService
    {
        public async Task CreateUserAsync(User user)
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

            await accountRepository.AddUserToDatabaseAsync(newUser);
        }

        public async Task<User> GetUserBasedOnEmail(User user) => await databaseContext.Users.FirstOrDefaultAsync(row => row.Email == user.Email);
    }
}
