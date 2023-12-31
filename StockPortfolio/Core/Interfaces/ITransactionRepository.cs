﻿using StockPortfolio.Core.Models;
using System.Collections.Generic;

namespace StockPortfolio.Core.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateTransaction(Transaction transaction);
        Task<IEnumerable<Transaction>> GetTransactionsByPorfolioId(Guid portfolioId);
    }
}