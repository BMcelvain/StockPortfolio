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

        public async Task<bool> DeletePortfolio(Portfolio portfolio)
        {
            try
            {
                _dbContext.Portfolios.Remove(portfolio);
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateException ex)
            {
                return false;
            }
            
        }

        public async Task<List<TransactionsWithStockDataModel>> GetPortfolio(Guid portfolioId)
        {
            var result = await (from t in _dbContext.Transactions
                                join s in _dbContext.Stocks
                                on new { Symbol = t.StockSymbol, Date = t.TransactionDate }
                                equals new { Symbol = s.StockSymbol, Date = s.UpdateDate }
                                where t.PortfolioId == portfolioId
                                select new TransactionsWithStockDataModel
                                {
                                    TransactionDate = t.TransactionDate,
                                    TransactionType = t.TransactionType,
                                    StockSymbol = t.StockSymbol,
                                    Shares = t.Shares,
                                    Price = t.Price,
                                    ClosePrice = s.ClosePrice,
                                    PercentChange = ((s.ClosePrice - s.OpenPrice)/ s.OpenPrice) * 100,
                                    Volume = s.Volume
                                }).ToListAsync();

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

        public async Task<Portfolio> GetPortfolioById(Guid portfolioId)
        {
            var result = await _dbContext.Portfolios
                                .Where(p => p.PortfolioId == portfolioId)
                                .FirstOrDefaultAsync();

            return result;
        }
    }
}
