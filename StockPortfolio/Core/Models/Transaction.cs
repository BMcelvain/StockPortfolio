namespace StockPortfolio.Core.Models
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid PortfolioID { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string StockSymbol { get; set; }
        public decimal Shares { get; set; }
        public decimal Price { get; set; }
    }
}
