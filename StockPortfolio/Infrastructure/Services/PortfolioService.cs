using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<Portfolio> GetPortfolio(Guid portfolioId)
        {
            var result = await _portfolioRepository.GetPortfolio(portfolioId);

            return result;
        }

        public async Task<List<Portfolio>> GetPortfolioList(string userId)
        {
            var result = await _portfolioRepository.GetPortfolioList(userId);
            
            return result;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            var result = await _portfolioRepository.CreatePortfolio(portfolio);

            return result;
        }

        public async Task<Portfolio> UpdatePortfolio(Portfolio portfolio)
        {
            var result = await _portfolioRepository.UpdatePortfolio(portfolio);

            return result;
        }

        public async Task<Portfolio> DeletePortfolio(Guid portfolioId)
        {
            var portfolioToDelete = await _portfolioRepository.GetPortfolio(portfolioId);

            if (portfolioToDelete == null)
            {
                return null;
            }

            var result = await _portfolioRepository.DeletePortfolio(portfolioToDelete);

            return result;
        }
    }
}
