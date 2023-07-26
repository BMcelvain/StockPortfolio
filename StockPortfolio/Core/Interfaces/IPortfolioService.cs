using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.ViewModels;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IPortfolioService
    {
        Task<IndividualPortfolioViewModel> GetPortfolio(Guid portfolioId);
        Task<List<Portfolio>> GetPortfolioList(string userId);
        Task<Portfolio> CreatePortfolio(Portfolio portfolio);
        Task<Portfolio> UpdatePortfolio(Portfolio portfolio);
        Task<bool> DeletePortfolio(Guid portfolioId);
    }
}
