using FinancialTransactions.Interface.Persistence;

namespace FinancialTransactions.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITransactionRepository transacations { get; }
        public UnitOfWork(ITransactionRepository transacations)
        {
            this.transacations = transacations ?? throw new ArgumentNullException(nameof(transacations));
        }
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }
    }
}
