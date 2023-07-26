using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> CreateStock(Stock stock);
        Task<Dictionary<string, decimal[]>> GetCurrentStockPrices(DateTime date);
    }
}
