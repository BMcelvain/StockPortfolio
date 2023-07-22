using System;

namespace StockPortfolio.Core.Models
{
    public class Stock
    {
        public string stockSymbol { get; set; }
        public DateTime updateDate { get; set; }
        public decimal openPrice { get; set; }
        public decimal highPrice { get; set; }
        public decimal lowPrice { get; set; }
        public decimal closePrice { get; set; }
        public int volume { get; set; }
    }
}
