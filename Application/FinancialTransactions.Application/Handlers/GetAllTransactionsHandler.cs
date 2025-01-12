using FinancialTransactions.Application.Bases;
using FinancialTransactions.Application.Queries;
using FinancialTransactions.Interface.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialTransactions.Application.Handlers
{
    public class GetAllTransactionsHandler : IRequestHandler<GetAllTransactionsQuery, BaseResponse<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllTransactionsHandler> _logger;
        public GetAllTransactionsHandler(IUnitOfWork unitOfWork, ILogger<GetAllTransactionsHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<string>> Handle(GetAllTransactionsQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<string>();
            _logger.LogInformation($"GenerateReportAsync");
            response.Data = await _unitOfWork.transacations.GenerateReportAsync();
            response.succcess = true;
            response.Message = "Report Generated";
            return response;
        }

        
    }
}
