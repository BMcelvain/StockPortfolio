namespace StockPortfolio.Core.Interfaces
{
    public interface IStockRepositroy
    {
        Task CreateStock(Models.Stock stock);
    }
}
