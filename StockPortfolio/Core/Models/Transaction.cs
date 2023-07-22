using StockPortfolio.Core.Enums;

namespace StockPortfolio.Core.Models
{
    public class Transaction
    {
        public Guid id { get; set; }
        public Guid portfolioID { get; set; }
        public DateTime transactionDate { get; set; }
        public TransactionType.Type transactionType { get; set; }
        public string stockSymbol { get; set; }
        public decimal shares { get; set; }
        public decimal price { get; set; }
    }
}
