using System;

namespace StockPortfolio.Core.Models
{
    public class Stock
    {
        public string StockSymbol { get; set; }
        public DateTime UpdateDate { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public int Volume { get; set; }
    }
}
