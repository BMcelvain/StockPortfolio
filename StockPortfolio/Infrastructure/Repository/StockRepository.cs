using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;
using StockPortfolio.Infrastructure.Data;

namespace StockPortfolio.Infrastructure.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _dbContext; 

        public StockRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<Stock> CreateStock(Stock stock)
        {
            _dbContext.Stocks.Add(stock);
            await _dbContext.SaveChangesAsync();

            return stock;
        }
    }
}
