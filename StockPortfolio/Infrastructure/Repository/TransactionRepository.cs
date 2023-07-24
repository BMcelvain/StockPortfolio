using Microsoft.EntityFrameworkCore;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;
using StockPortfolio.Infrastructure.Data;

namespace StockPortfolio.Infrastructure.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TransactionRepository(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        public async Task<Transaction> CreateTransaction(Transaction transaction)
        {
            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }
          
        public async Task<IEnumerable<Transaction>> GetTransactionsByPorfolioId(Guid portfolioId)
        {
            var result = await _dbContext.Transactions
                .Where(t => t.PortfolioID == portfolioId)
                .ToListAsync();

            return result;
        }
    }
}
