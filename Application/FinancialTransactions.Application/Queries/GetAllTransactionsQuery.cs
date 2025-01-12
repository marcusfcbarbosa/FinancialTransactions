using FinancialTransactions.Application.Bases;
using FinancialTransactions.Domain.Entities;
using MediatR;

namespace FinancialTransactions.Application.Queries
{
    public class GetAllTransactionsQuery : IRequest<BaseResponse<string>>
    {

    }
}
