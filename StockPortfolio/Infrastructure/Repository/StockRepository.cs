using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Repository
{
    public class StockRepository : IStockRepositroy
    {
        Task IStockRepositroy.CreateStock(Stock stock)
        {
            throw new NotImplementedException();
        }
    }
}
