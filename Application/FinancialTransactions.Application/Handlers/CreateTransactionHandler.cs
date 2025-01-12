using AutoMapper;
using FinancialTransactions.Application.Bases;
using FinancialTransactions.Application.Commands;
using FinancialTransactions.Interface.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinancialTransactions.Application.Handlers
{
    public class CreateTransactionHandler : IRequestHandler<CreateTransactionCommand, BaseResponse<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTransactionHandler> _logger;
        public CreateTransactionHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateTransactionHandler> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<BaseResponse<bool>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseResponse<bool>();
            try
            {
                _logger.LogInformation($"AddTransacation");
                response.Data = await _unitOfWork.transacations.AddTransacation(request.filePath);
                response.succcess = true;
                response.Message = "Transactions created";
            }
            catch (Exception ex) {
                response.succcess = false;
                _logger.LogInformation($"Error during the process {ex.Message}");
            }
            return response;
        }
    }
}
