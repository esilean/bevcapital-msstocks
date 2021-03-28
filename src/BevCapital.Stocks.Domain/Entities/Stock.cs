using BevCapital.Stocks.Domain.Core.EventStores.Aggregate;
using BevCapital.Stocks.Domain.Events.StockEvents;
using FluentValidation;
using System;
using System.Collections.Generic;

namespace BevCapital.Stocks.Domain.Entities
{
    public class Stock : Aggregate
    {
        public string StockName { get; private set; }
        public string Exchange { get; private set; }
        public string Website { get; private set; }
        public StockPrice StockPrice { get; private set; }
        public ICollection<AppUserStock> AppUserStocks { get; set; }

        /// <summary>
        /// EF Constructor
        /// </summary>
        protected Stock() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="stockName"></param>
        /// <param name="exchange"></param>
        /// <param name="website"></param>
        private Stock(string id, string stockName, string exchange, string website)
        {
            Id = id;
            StockName = stockName;
            Exchange = exchange;
            Website = website;
            InitPrice();
            Validate(this, new StockValidator());

            AddStockToEnqueue();
        }

        public static Stock Create(string symbol, string name, string exchange, string website)
        {
            return new Stock(symbol, name, exchange, website);
        }

        /// <summary>
        /// This method is used by the eventstore when creating the object by reflection
        /// </summary>
        /// <param name="event"></param>
        private void Apply(StockCreatedEvent @event)
        {
            Id = @event.Id;
            StockName = @event.StockName;
            Exchange = @event.Exchange;
            Website = @event.Website;
            StockPrice = StockPrice.Create(@event.StockPrice.Id,
                                           @event.StockPrice.Open,
                                           @event.StockPrice.Close,
                                           @event.StockPrice.High,
                                           @event.StockPrice.Low,
                                           @event.StockPrice.LatestPrice,
                                           @event.StockPrice.LatestPriceTime,
                                           @event.StockPrice.DelayedPrice,
                                           @event.StockPrice.DelayedPriceTime,
                                           @event.StockPrice.PreviousClosePrice,
                                           @event.StockPrice.ChangePercent);
        }

        public void UpdateStock(string stockName, string exchange, string website)
        {
            StockName = stockName;
            Exchange = exchange;
            Website = website;

            var @event = new StockUpdatedEvent
            {
                Id = Id,
                StockName = stockName,
                Exchange = exchange,
                Website = website,
                StockPrice = new StockPriceEvent
                {
                    Id = StockPrice.Id,
                    Open = StockPrice.Open,
                    Close = StockPrice.Close,
                    High = StockPrice.High,
                    Low = StockPrice.Low,
                    LatestPrice = StockPrice.LatestPrice,
                    LatestPriceTime = StockPrice.LatestPriceTime,
                    DelayedPrice = StockPrice.DelayedPrice,
                    DelayedPriceTime = StockPrice.DelayedPriceTime,
                    PreviousClosePrice = StockPrice.PreviousClosePrice,
                    ChangePercent = StockPrice.ChangePercent
                }
            };

            Enqueue(@event);
        }

        public void RemoveStockToEnqueue()
        {
            var @event = new StockDeletedEvent
            {
                Id = Id,
                StockName = StockName,
                Exchange = Exchange,
                Website = Website,
                StockPrice = new StockPriceEvent
                {
                    Id = StockPrice.Id,
                    Open = StockPrice.Open,
                    Close = StockPrice.Close,
                    High = StockPrice.High,
                    Low = StockPrice.Low,
                    LatestPrice = StockPrice.LatestPrice,
                    LatestPriceTime = StockPrice.LatestPriceTime,
                    DelayedPrice = StockPrice.DelayedPrice,
                    DelayedPriceTime = StockPrice.DelayedPriceTime,
                    PreviousClosePrice = StockPrice.PreviousClosePrice,
                    ChangePercent = StockPrice.ChangePercent
                }
            };

            Enqueue(@event);
        }

        private void AddStockToEnqueue()
        {
            var @event = new StockCreatedEvent
            {
                Id = Id,
                StockName = StockName,
                Exchange = Exchange,
                Website = Website,
                StockPrice = new StockPriceEvent
                {
                    Id = StockPrice.Id,
                    Open = StockPrice.Open,
                    Close = StockPrice.Close,
                    High = StockPrice.High,
                    Low = StockPrice.Low,
                    LatestPrice = StockPrice.LatestPrice,
                    LatestPriceTime = StockPrice.LatestPriceTime,
                    DelayedPrice = StockPrice.DelayedPrice,
                    DelayedPriceTime = StockPrice.DelayedPriceTime,
                    PreviousClosePrice = StockPrice.PreviousClosePrice,
                    ChangePercent = StockPrice.ChangePercent
                }
            };

            Enqueue(@event);
        }

        private void InitPrice()
        {
            StockPrice = StockPrice.Create(Id,
                                           0, 0, 0, 0,
                                           0, DateTime.UtcNow,
                                           0, DateTime.UtcNow,
                                           0, 0);
        }

        public void SetPrice(decimal open, decimal close, decimal high, decimal low,
                             decimal latestPrice, DateTime latestPriceTime,
                             decimal delayedPrice, DateTime delayedPriceTime,
                             decimal previousClosePrice, decimal? changePercent)
        {
            StockPrice = StockPrice.Create(Id,
                                           open, close, high, low,
                                           latestPrice, latestPriceTime,
                                           delayedPrice, delayedPriceTime,
                                           previousClosePrice, changePercent);
            AddStockToEnqueue();
        }

        internal class StockValidator : AbstractValidator<Stock>
        {
            public StockValidator()
            {
                RuleFor(a => a.Id)
                    .NotEmpty()
                    .WithMessage("Invalid symbol");

                RuleFor(a => a.StockName)
                    .NotEmpty()
                    .WithMessage("Invalid stock name");

                RuleFor(a => a.Exchange)
                    .NotEmpty()
                    .WithMessage("Invalid exchange");

                RuleFor(a => a.Website)
                    .NotEmpty()
                    .WithMessage("Invalid website");
            }
        }
    }
}
