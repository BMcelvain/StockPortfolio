using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;
using StockPortfolio.Infrastructure.Data;

namespace StockPortfolio.Infrastructure.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PortfolioRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;  
        }
        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            _dbContext.Portfolios.Add(portfolio);
            await _dbContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio> DeletePortfolio(Portfolio portfolio)
        {
            _dbContext.Portfolios.Remove(portfolio);
            await _dbContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio> GetPortfolio(Guid portfolioId)
        { 
            var result = await _dbContext.Portfolios
                                .Where(p => p.PortfolioId == portfolioId)
                                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Portfolio>> GetPortfolioList(string userId)
        {
            var result = await _dbContext.Portfolios
                                .Where(p => p.UserId == userId)
                                .ToListAsync();

            return result;
        }

        public async Task<Portfolio> UpdatePortfolio(Portfolio portfolio)
        {
            var existingPortfolio = await _dbContext.Portfolios
                                            .Where(p => p.PortfolioId == portfolio.PortfolioId)
                                            .FirstOrDefaultAsync();

            if (existingPortfolio == null)
            {
                return null;
            }

            existingPortfolio.PortfolioName = portfolio.PortfolioName;
            existingPortfolio.Balance = portfolio.Balance;
            await _dbContext.SaveChangesAsync();

            return portfolio;
        }
    }
}
