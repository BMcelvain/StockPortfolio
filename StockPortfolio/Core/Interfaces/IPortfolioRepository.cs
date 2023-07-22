namespace StockPortfolio.Core.Interfaces
{
    public interface IPortfolioRepository
    {
        Task CreatePortfolio(Models.Portfolio portfolio);
        Task<IEnumerable<Models.Portfolio>> GetPortfoliosByUserId(Guid userId);
        Task UpdatePortfolio(Guid portfolioId);
        Task DeletePortfolio(Guid portfolioId);
    }
}
