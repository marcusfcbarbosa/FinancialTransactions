using CsvHelper;
using CsvHelper.Configuration;
using Dapper;
using FinancialTransactions.Infrastructure.Contexts;
using FinancialTransactions.Interface.Persistence;
using System.Data;
using System.Globalization;
using System.Text.Json;

namespace FinancialTransactions.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly DatabaseContext _dbContext;

        public TransactionRepository(DatabaseContext applicationContext)
        {
            _dbContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
        }

        public async Task<bool> AddTransacation(string filepath)
        {
            var records = new List<FinancialTransactions.Domain.Entities.Transaction>();
            using var connection = _dbContext.CreateConnection();
            using (var reader = new StreamReader(filepath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                await foreach (var transaction in csv.GetRecordsAsync<FinancialTransactions.Domain.Entities.Transaction>())
                {
                    records.Add(transaction);
                    if (records.Count >= 1000)
                    {
                        await _dbContext.Transactions.AddRangeAsync(records);
                        await _dbContext.SaveChangesAsync();
                        records.Clear();
                    }
                }

                if (records.Any())
                {
                    await _dbContext.Transactions.AddRangeAsync(records);
                    await _dbContext.SaveChangesAsync();
                }

                return true;
            }
        }
        public async Task<string> GenerateReportAsync()
        {
            using (var connection = _dbContext.CreateConnection())
            {
                var usersSummary = await connection.QueryAsync<dynamic>(@"
                SELECT UserId AS user_id, 
                    SUM(CASE WHEN Amount > 0 THEN Amount ELSE 0 END) AS total_income, 
                    SUM(CASE WHEN Amount < 0 THEN Amount ELSE 0 END) AS total_expense
                FROM Transactions
                GROUP BY UserId",
                    commandType: CommandType.Text);

                var topCategories = await connection.QueryAsync<dynamic>(@"
                SELECT TOP 3 Category AS category, COUNT(*) AS transactions_count
                FROM Transactions
                GROUP BY Category
                ORDER BY transactions_count DESC",
                    commandType: CommandType.Text);

                
                var highestSpender = await connection.QueryFirstOrDefaultAsync<dynamic>(@"
                SELECT TOP 1 UserId AS user_id, 
                    SUM(Amount) AS total_expense
                FROM Transactions
                WHERE Amount < 0
                GROUP BY UserId
                ORDER BY total_expense DESC",
                    commandType: CommandType.Text);

                var report = new
                {
                    users_summary = usersSummary,
                    top_categories = topCategories,
                    highest_spender = highestSpender
                };
                return JsonSerializer.Serialize(report);
            }
        }

    }
}
