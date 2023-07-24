using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.Models;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpGet("{portfolioId}")]
        public async Task<IActionResult> GetPortfolio(Guid portfolioId)
        {
            try
            {
                var portfolio = await _portfolioService.GetPortfolio(portfolioId);

                if (portfolio == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                return Ok(portfolio);
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("list/{userId}")]
        public async Task<IActionResult> GetPortfolioList(string userId)
        {
            try
            {
                var portfolioList = await _portfolioService.GetPortfolioList(userId);

                if (portfolioList == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                return Ok(portfolioList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolio([FromBody] Portfolio portfolio)
        {
            try
            {
                await _portfolioService.CreatePortfolio(portfolio);

                return Ok(new Response { Status = "Success", Message = "Portfolio created successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePortfolio([FromBody] Portfolio portfolio)
        {
            try
            {
                var result = await _portfolioService.UpdatePortfolio(portfolio);

                if (result == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                return Ok(new Response { Status = "Success", Message = "Portfolio updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePortfolio([FromBody] Guid portfolioId)
        {
            try
            {
                var portfolioToDelete = await _portfolioService.DeletePortfolio(portfolioId);

                if (portfolioToDelete == null)
                {
                    return NotFound(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                return Ok(new Response { Status = "Success", Message = "Portfolio deleted successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
