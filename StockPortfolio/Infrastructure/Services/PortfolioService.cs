using Microsoft.AspNetCore.Mvc;
using StockPortfolio.Api.ViewModels;
using StockPortfolio.Core.Interfaces;
using StockPortfolio.Core.Models;

namespace StockPortfolio.Infrastructure.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;
        private readonly IStockRepository _stockRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository, IStockRepository stockRepository)
        {
            _portfolioRepository = portfolioRepository;
            _stockRepository = stockRepository;
        }

        public async Task<IndividualPortfolioViewModel> GetPortfolio(Guid portfolioId)
        {
            var transactionsWithStockData = await _portfolioRepository.GetPortfolio(portfolioId);
            var currentStockData = await _stockRepository.GetCurrentStockPrices(new DateTime(2023, 7, 14));
            var stockData = GetSharesAndCostPerShare(transactionsWithStockData);

            foreach(var stock in stockData.Keys)
            {
                stockData[stock][2] = currentStockData[stock][0]; // current price
                stockData[stock][3] = currentStockData[stock][1]; // current volume
                stockData[stock][4] = CalculatePercentGainLoss(currentStockData[stock][0], stockData[stock][1]);
            }

            var result = new IndividualPortfolioViewModel()
            {
                PortfolioId = portfolioId,
                TransactionsWithStockData = transactionsWithStockData,
                StockData = stockData
            };

            return result;
        }

        public async Task<List<Portfolio>> GetPortfolioList(string userId)
        {
            var result = await _portfolioRepository.GetPortfolioList(userId);
            
            return result;
        }

        public async Task<Portfolio> CreatePortfolio(Portfolio portfolio)
        {
            var result = await _portfolioRepository.CreatePortfolio(portfolio);

            return result;
        }

        public async Task<Portfolio> UpdatePortfolio(Portfolio portfolio)
        {
            var result = await _portfolioRepository.UpdatePortfolio(portfolio);

            return result;
        }

        public async Task<bool> DeletePortfolio(Guid portfolioId)
        {
            var portfolioToDelete = await _portfolioRepository.GetPortfolioById(portfolioId);

            if (portfolioToDelete == null)
            {
                return false;
            }

            var result = await _portfolioRepository.DeletePortfolio(portfolioToDelete);

            return true;
        }


        // Helpers
        private Dictionary<string, decimal[]> GetSharesAndCostPerShare(List<TransactionsWithStockDataModel> transactionsWithStockData)
        {
            // stockData format:     Stock Symbol, [Shares, Avg. Cost Per Share, Current Price]
            var stockData = new Dictionary<string, decimal[]>();

            foreach(var transaction in transactionsWithStockData)  
            { 
                var stockSymbol = transaction.StockSymbol;

                if (transaction.TransactionType == "Buy")
                {

                    // If stock symbol not in dictionary, add it.
                    if (stockData.ContainsKey(stockSymbol) == false)
                    {
                        stockData.Add(stockSymbol, new decimal[5]);
                    }

                    // Calculate average cost per share.
                    if (stockData[stockSymbol][1] == 0)
                    {
                        stockData[stockSymbol][1] = transaction.Price;
                    }
                    else
                    {
                        // Avg. Cost Per Share = ((Shares * Avg. Cost Per Share) + (New Transaction Shares * Price Per Share)) / (Shares + New Transaction Shares)
                        stockData[stockSymbol][1] = ((stockData[stockSymbol][1] * stockData[stockSymbol][0]) + (transaction.Shares * transaction.Price)) / (stockData[stockSymbol][0] + transaction.Shares);
                        stockData[stockSymbol][1] = Math.Round(stockData[stockSymbol][1], 2);
                    }

                    // Add number of shares from transaction.
                    stockData[stockSymbol][0] += transaction.Shares;
                }
                else
                {
                    stockData[stockSymbol][0] -= transaction.Shares;

                    // If shares = 0, remove stock symbol from dictionary.
                    if (stockData[stockSymbol][0] == 0)
                    {
                        stockData.Remove(stockSymbol);
                        continue;
                    }

                    // Adjusted cost basis = ((Shares * Avg. Cost Per Share) - (Shares Sold * Sale Price Per Share)) / (Shares - Shares Sold)
                    stockData[stockSymbol][1] = ((stockData[stockSymbol][0] * stockData[stockSymbol][1]) - (transaction.Shares * transaction.Price)) / (stockData[stockSymbol][0] - transaction.Shares);
                }
            }

            return stockData;
        }

        private decimal CalculatePercentGainLoss(decimal currentStockPrice, decimal avgCostPerShare)
        {
            // Percent Gain/Loss = ((Current Price - Avg. Cost Per Share) / Avg. Cost Per Share) * 100
            var percentGainLoss = ((currentStockPrice - avgCostPerShare) / avgCostPerShare) * 100;
            percentGainLoss = Math.Round(percentGainLoss, 2);

            return percentGainLoss;
        }
    }
}
