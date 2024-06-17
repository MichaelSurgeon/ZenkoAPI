using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Models;

namespace ZenkoAPI.Data
{
    public class DatabaseContext : DbContext
    {
        private IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQLConnectionString"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CalculatedCategories> CalculatedCategories { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<AggregatedTransactions> AggregatedTransactions { get; set; }
    }
}
