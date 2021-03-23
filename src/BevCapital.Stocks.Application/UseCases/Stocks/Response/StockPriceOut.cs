using System;

namespace BevCapital.Stocks.Application.UseCases.Stocks.Response
{
    public class StockPriceOut
    {
        public decimal Open { get; set; }
        public decimal Close { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal LatestPrice { get; set; }
        public DateTime LatestPriceTime { get; set; }
        public decimal DelayedPrice { get; set; }
        public DateTime DelayedPriceTime { get; set; }
        public decimal PreviousClosePrice { get; set; }
        public decimal? ChangePercent { get; set; }
    }
}
