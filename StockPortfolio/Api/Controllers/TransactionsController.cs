using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            try
            {
                var result = await _transactionService.CreateTransaction(transaction);

                if (result == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Transaction not created!" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("{portfolioId}")]
        public async Task<IActionResult> GetTransactionsByPorfolioId(Guid portfolioId)
        {
            try
            {
                var result = await _transactionService.GetTransactionsByPorfolioId(portfolioId);

                if (result == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Transactions not found!" });
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
