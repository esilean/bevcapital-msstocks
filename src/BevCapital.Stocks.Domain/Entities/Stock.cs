using FluentValidation;
using System;
using System.Collections.Generic;

namespace BevCapital.Stocks.Domain.Entities
{
    public class Stock : Entity
    {
        public string Symbol { get; private set; }
        public string Name { get; private set; }
        public string Exchange { get; private set; }
        public string Website { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public StockPrice StockPrice { get; private set; }
        public ICollection<AppUserStock> AppUserStocks { get; set; }

        /// <summary>
        /// EF Constructor
        /// </summary>
        protected Stock() { }

        private Stock(string symbol, string name, string exchange, string website, DateTime createdAt, DateTime updatedAt)
        {
            Symbol = symbol;
            Name = name;
            Exchange = exchange;
            Website = website;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;

            Validate(this, new StockValidator());
        }

        public static Stock Create(string symbol, string name, string exchange, string website)
        {
            return new Stock(symbol, name, exchange, website, DateTime.Now.ToUniversalTime(), DateTime.Now.ToUniversalTime());
        }

        public void InitPrice()
        {
            StockPrice = StockPrice.Create(Symbol,
                                           0, 0, 0, 0,
                                           0, DateTime.Now.ToUniversalTime(),
                                           0, DateTime.Now.ToUniversalTime(),
                                           0, 0);
        }

        public void SetPrice(decimal open, decimal close, decimal high, decimal low,
                             decimal latestPrice, DateTime latestPriceTime,
                             decimal delayedPrice, DateTime delayedPriceTime,
                             decimal previousClosePrice, decimal? changePercent)
        {
            StockPrice = StockPrice.Create(Symbol,
                                           open, close, high, low,
                                           latestPrice, latestPriceTime,
                                           delayedPrice, delayedPriceTime,
                                           previousClosePrice, changePercent);
        }

        internal class StockValidator : AbstractValidator<Stock>
        {
            public StockValidator()
            {
                RuleFor(a => a.Symbol)
                    .NotEmpty()
                    .WithMessage("Invalid symbol");

                RuleFor(a => a.Name)
                    .NotEmpty()
                    .WithMessage("Invalid name");

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
