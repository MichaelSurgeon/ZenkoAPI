using ZenkoAPI.Data;
using ZenkoAPI.Models;


namespace ZenkoAPI.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DatabaseContext _databaseContext;

        public AccountRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task AddUserToDatabaseAsync(User user)
        {
            try
            {
                await _databaseContext.AddAsync(user);
                await _databaseContext.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
