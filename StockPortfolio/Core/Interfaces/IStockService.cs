using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IStockService
    {
        Task<Stock> CreateStock(Stock stock);
    }
}
