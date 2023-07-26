using StockPortfolio.Core.Models;

namespace StockPortfolio.Api.ViewModels
{
    public class IndividualPortfolioViewModel
    {
        public Guid PortfolioId { get; set; }
        public string PortfolioName { get; set; }
        public decimal Balance { get; set; }
        public string UserId { get; set; }
        public List<TransactionsWithStockDataModel> TransactionsWithStockData { get; set; }
        public Dictionary<string, decimal[]> StockData { get; set; }
    }
}