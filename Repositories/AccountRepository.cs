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
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error adding user to database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding user", ex);
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
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error deleting user from database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting user", ex);
            }
        }

        public async Task<User> GetUserAsync(User user)
        {
            try
            {
                return await databaseContext.Users.FirstOrDefaultAsync(row => row.Email.ToLower() == user.Email.ToLower());
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("Error deleting user from database", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting user", ex);
            }
        }
    }
}
