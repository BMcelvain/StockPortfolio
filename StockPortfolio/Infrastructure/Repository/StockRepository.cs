using Microsoft.EntityFrameworkCore;
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

        public async Task<Dictionary<string, decimal[]>> GetCurrentStockPrices(DateTime date)
        {
            var formattedDate = date.ToString("yyyy-MM-dd");
            var stockPricesOnDate = await _dbContext.Stocks
                                .Where(s => s.UpdateDate == date)
                                .Select(s => new Stock
                                {
                                    StockSymbol = s.StockSymbol,
                                    ClosePrice = s.ClosePrice,
                                    Volume = s.Volume   
                                }).ToListAsync();

            var result = stockPricesOnDate.ToDictionary(s => s.StockSymbol, s => new decimal[] { s.ClosePrice, s.Volume });

            return result;
        }
    }
}
