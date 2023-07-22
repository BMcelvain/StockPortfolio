using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Repository
{
    public class PortfolioRepository : IPortfolioRepository
    {
        Task IPortfolioRepository.CreatePortfolio(Portfolio portfolio)
        {
            throw new NotImplementedException();
        }

        Task IPortfolioRepository.DeletePortfolio(Guid portfolioId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Portfolio>> IPortfolioRepository.GetPortfoliosByUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        Task IPortfolioRepository.UpdatePortfolio(Guid portfolioId)
        {
            throw new NotImplementedException();
        }
    }
}
