using BevCapital.Stocks.Domain.Core.Events;
using FluentValidation;

namespace BevCapital.Stocks.Domain.Events.StockEvents
{
    public class StockEvent : IEvent
    {
        public string Id { get; set; }
        public string StockName { get; set; }
        public string Exchange { get; set; }
        public string Website { get; set; }
        public StockPriceEvent StockPrice { get; set; }

        public class Validator : AbstractValidator<StockEvent>
        {
            public Validator()
            {
                RuleFor(e => e.Id).NotEmpty();
                RuleFor(e => e.StockName).NotEmpty();
                RuleFor(e => e.Exchange).NotEmpty();
                RuleFor(e => e.Website).NotEmpty();
                RuleFor(e => e.StockPrice).NotEmpty();
            }
        }
    }
}
