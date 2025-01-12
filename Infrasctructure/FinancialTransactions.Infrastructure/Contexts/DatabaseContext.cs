using FinancialTransactions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FinancialTransactions.Infrastructure.Contexts
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbSet<Transaction> Transactions { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options,
            IConfiguration configuration)
         : base(options)
        {
            _connectionString = configuration.GetConnectionString("DatabaseConnection") ?? throw new ArgumentNullException("Connection string not found.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}
