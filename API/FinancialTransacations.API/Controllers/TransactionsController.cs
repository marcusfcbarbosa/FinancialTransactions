using FinancialTransactions.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransacations.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<TransactionsController> _logger;
        public TransactionsController(IMediator mediator, ILogger<TransactionsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }

        /// <summary>
        /// It does not open the swagger uf u want u need to send directly trough POSTMAN
        /// </summary>
        /// <returns></returns>
        //[HttpPost(Name = "upload")]
        //[SwaggerOperation(Summary = "Upload a CSV file to create transactions")]
        //[Consumes("multipart/form-data")]
        //public async Task<IActionResult> UploadCsv([FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);
        //    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await file.CopyToAsync(stream);
        //    }
        //    var response = await _mediator.Send(new CreateTransactionCommand { filePath = filePath });
        //    return Ok(response);
        //}

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _mediator.Send(new GetAllTransactionsQuery());
            if (response.succcess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

    }
}
