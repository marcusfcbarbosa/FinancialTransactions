
namespace FinancialTransactions.Interface.Persistence
{
    public interface ITransactionRepository
    {
        Task<string> GenerateReportAsync(); 
        Task<bool> AddTransacation(string filepath);
    }
}
