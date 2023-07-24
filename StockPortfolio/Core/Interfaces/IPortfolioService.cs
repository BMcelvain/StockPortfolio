using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IPortfolioService
    {
        Task<Portfolio> GetPortfolio(Guid portfolioId);
        Task<List<Portfolio>> GetPortfolioList(string userId);
        Task<Portfolio> CreatePortfolio(Portfolio portfolio);
        Task<Portfolio> UpdatePortfolio(Portfolio portfolio);
        Task<Portfolio> DeletePortfolio(Guid portfolio);
    }
}
