using System.Collections.Generic;

namespace StockPortfolio.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task CreateTransaction(Models.Transaction transaction);
        Task<IEnumerable<Models.Transaction>> GetTransactionsByPorfolioId(Guid portfolioId);

    }
}
