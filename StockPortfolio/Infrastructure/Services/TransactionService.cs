using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            var result = await _transactionRepository.CreateTransaction(transaction);

            return result;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByPorfolioId(Guid portfolioId)
        {
            var result = await _transactionRepository.GetTransactionsByPorfolioId(portfolioId);

            return result;
        }
    }
}
