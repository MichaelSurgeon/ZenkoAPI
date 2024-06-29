using Microsoft.EntityFrameworkCore;
using ZenkoAPI.Models;

namespace ZenkoAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CalculatedCategories> CalculatedCategories { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<AggregatedTransaction> AggregatedTransactions { get; set; }
    }
}
