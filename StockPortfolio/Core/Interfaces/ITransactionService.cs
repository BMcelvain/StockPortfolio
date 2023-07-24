using StockPortfolio.Core.Models;

namespace StockPortfolio.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByPorfolioId(Guid portfolioId);
    }
}
