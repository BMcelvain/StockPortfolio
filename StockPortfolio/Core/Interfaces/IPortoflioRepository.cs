using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface IPortfolioRepository
    {
        Task<List<TransactionsWithStockDataModel>> GetPortfolio(Guid portfolioId);
        Task<List<Portfolio>> GetPortfolioList(string userId);
        Task<Portfolio> CreatePortfolio(Portfolio portfolio);
        Task<Portfolio> UpdatePortfolio(Portfolio portfolio);
        Task<bool> DeletePortfolio(Portfolio portfolio);
        Task<Portfolio> GetPortfolioById(Guid portfolioId);
    }
}
