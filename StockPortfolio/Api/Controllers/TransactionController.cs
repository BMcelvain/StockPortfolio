using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Api.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
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

        [HttpPost]
        public async Task<ActionResult<List<Transaction>>> GetTransactionsByPortfolioId(Guid portfolioId)
        {
            var result = await _transactionService.GetTransactionsByPorfolioId(portfolioId);

            if (result == null)
            {
                return View(new Response { Status = "Error", Message = "Transactions not found!" });
            }

            return View(result);
        }
    }
}
