using FinancialTransactions.Application.Bases;
using MediatR;

namespace FinancialTransactions.Application.Commands
{
    public class CreateTransactionCommand : IRequest<BaseResponse<bool>>
    {
        public string filePath { get; set; }
    }
}
