using FinancialTransactions.Infrastructure.Contexts;
using FinancialTransactions.Infrastructure.Repositories;
using FinancialTransactions.Interface.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FinancialTransactions.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInjectionPersistence(this IServiceCollection services)
        {
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
