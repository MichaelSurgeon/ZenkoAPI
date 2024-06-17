using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Models;

namespace ZenkoAPI.Data
{
    public class DatabaseContext : DbContext
    {
        private IConfigurationManager _configurationManager;

        public DatabaseContext(IConfigurationManager configurationManager)
        {
            _configurationManager = configurationManager;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configurationManager.GetConnectionString("PostgreSQLConnectionString"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CalculatedCategories> CalculatedCategories { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<AggregatedTransactions> AggregatedTransactions { get; set; }
    }
}
