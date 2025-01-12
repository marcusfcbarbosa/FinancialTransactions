namespace FinancialTransactions.Interface.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        public ITransactionRepository transacations { get; }


    }
}
