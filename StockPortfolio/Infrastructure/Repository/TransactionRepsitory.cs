using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Repository
{
    public class TransactionRepsitory : ITransactionRepository
    {
        Task ITransactionRepository.CreateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Transaction>> ITransactionRepository.GetTransactionsByPorfolioId(Guid portfolioId)
        {
            throw new NotImplementedException();
        }
    }
}
