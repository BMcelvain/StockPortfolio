namespace StockPortfolio.Core.Models
{
    public class TransactionsWithStockDataModel
    {
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public string StockSymbol { get; set; }
        public decimal Shares { get; set; }
        public decimal Price { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal PercentChange { get; set; }
        public int Volume { get; set; }
    }
}
