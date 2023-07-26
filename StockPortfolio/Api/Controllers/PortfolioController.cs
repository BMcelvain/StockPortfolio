using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.Models;
using StockPortfolio.Api.ViewModels;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Api.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IPortfolioService _portfolioService;

        public PortfolioController(IPortfolioService portfolioService)
        {
            _portfolioService = portfolioService;
        }

        [HttpPost]
        public async Task<ActionResult<IndividualPortfolioViewModel>> GetPortfolio(Portfolio portfolio)
        {
            try
            {
                var portfolioData = await _portfolioService.GetPortfolio(portfolio.PortfolioId);

                if (portfolio == null)
                {
                    return View(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                portfolioData.PortfolioId = portfolio.PortfolioId;
                portfolioData.PortfolioName = portfolio.PortfolioName;
                portfolioData.UserId = portfolio.UserId;
                portfolioData.Balance = portfolio.Balance;
                return View(portfolioData);
            } 
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpGet("list")]
        public async Task<ActionResult<List<Portfolio>>> GetPortfolioList()
        {
            /////////////////////////////////////////////////////
            ///TO-DO: Remove this whenever JWT is up and running!
            /////////////////////////////////////////////////////
            string userId = "24cd6d4b-09ac-4080-924f-8e41822dca22";

            if (ModelState.IsValid) 
            {
                var portfolioList = await _portfolioService.GetPortfolioList(userId);

                if (portfolioList == null)
                {
                    return View(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                return View(portfolioList);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolioForm(Portfolio portfolio)
        {
            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePortfolio(Portfolio portfolio)
        {
            /////////////////////////////////////////////////////
            ///TO-DO: Remove this whenever JWT is up and running!
            /////////////////////////////////////////////////////
            string userId = "24cd6d4b-09ac-4080-924f-8e41822dca22";

            portfolio.UserId = userId;
            portfolio.PortfolioId = Guid.NewGuid();

            await _portfolioService.CreatePortfolio(portfolio);

            return RedirectToAction("GetPortfolioList", "Portfolio");
              

        }

        [HttpPost]
        public async Task<IActionResult> UpdatePortfolioForm(Portfolio portfolio)
        {
            return View(portfolio);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePortfolio(Portfolio portfolio)
        {
            try
            {
                var result = await _portfolioService.UpdatePortfolio(portfolio);

                if (result == null)
                {
                    return View(new Response { Status = "Error", Message = "Portfolio not found!" });
                }

                return RedirectToAction("GetPortfolioList", "Portfolio");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeletePortfolio(Guid portfolioId)
        {
            try
            {
                var portfolioDeleted = await _portfolioService.DeletePortfolio(portfolioId);

                if (!portfolioDeleted)
                {
                    return RedirectToAction("GetPortfolioList", "Portfolio");
                }

                return RedirectToAction("GetPortfolioList", "Portfolio");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = ex.Message });
            }
        }
    }
}
