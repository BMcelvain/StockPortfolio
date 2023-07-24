using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock> CreateStock(Stock stock);
    }
}
