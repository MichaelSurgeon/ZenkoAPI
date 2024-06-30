using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Data;
using ZenkoAPI.Models;

namespace ZenkoAPI.Repositories
{
    public class AccountRepository(DatabaseContext databaseContext) : IAccountRepository
    {
        public async Task<bool> AddUserToDatabaseAsync(User user)
        {
            try
            {
                await databaseContext.AddAsync(user);
                await databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user from database", ex);
            }
        }

        public async Task<bool> DeleteUserFromDatabaseAsync(User user)
        {
            try
            {
                databaseContext.Remove(user);
                await databaseContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting user from database", ex);
            }
        }

        public async Task<User> GetUserAsync(User user) => await databaseContext.Users.FirstOrDefaultAsync(row => row.Email.ToLower() == user.Email.ToLower());
        public async Task<User> GetUserByIdAsync(Guid userId) => await databaseContext.Users.FirstOrDefaultAsync(row => row.UserId == userId);
    }
}
