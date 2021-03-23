using System;

namespace BevCapital.Stocks.Domain.Entities
{
    public class StockPrice : Entity
    {
        public string Symbol { get; private set; }
        public Stock Stock { get; private set; }
        public decimal Open { get; private set; }
        public decimal Close { get; private set; }
        public decimal High { get; private set; }
        public decimal Low { get; private set; }
        public decimal LatestPrice { get; private set; }
        public DateTime LatestPriceTime { get; private set; }
        public decimal DelayedPrice { get; private set; }
        public DateTime DelayedPriceTime { get; private set; }
        public decimal PreviousClosePrice { get; private set; }
        public decimal? ChangePercent { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }

        /// <summary>
        /// EF Constructor
        /// </summary>
        protected StockPrice() { }

        public StockPrice(string symbol, 
                          decimal open, decimal close, decimal high, decimal low, 
                          decimal latestPrice, DateTime latestPriceTime, 
                          decimal delayedPrice, DateTime delayedPriceTime, 
                          decimal previousClosePrice, decimal? changePercent, 
                          DateTime createdAt, DateTime updatedAt)
        {
            Symbol = symbol;
            Open = open;
            Close = close;
            High = high;
            Low = low;
            LatestPrice = latestPrice;
            LatestPriceTime = latestPriceTime;
            DelayedPrice = delayedPrice;
            DelayedPriceTime = delayedPriceTime;
            PreviousClosePrice = previousClosePrice;
            ChangePercent = changePercent;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public static StockPrice Create(string symbol,
                                        decimal open, decimal close, decimal high, decimal low,
                                        decimal latestPrice, DateTime latestPriceTime,
                                        decimal delayedPrice, DateTime delayedPriceTime,
                                        decimal previousClosePrice, decimal? changePercent)
        {
            return new StockPrice(symbol,
                                  open, close, high, low,
                                  latestPrice, latestPriceTime,
                                  delayedPrice,delayedPriceTime,
                                  previousClosePrice, changePercent,
                                  DateTime.Now, DateTime.Now);
        }
    }
}
