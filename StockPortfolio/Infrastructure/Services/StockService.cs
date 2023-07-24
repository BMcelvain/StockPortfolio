using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepositroy)
        {
            _stockRepository = stockRepositroy;
        }

        public async Task<Stock> CreateStock(Stock stock)
        {
            var result = await _stockRepository.CreateStock(stock);

            return result;
        }
    }
}
