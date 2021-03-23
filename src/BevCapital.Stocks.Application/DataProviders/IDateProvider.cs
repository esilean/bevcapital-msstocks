using System;

namespace BevCapital.Stocks.Application.DataProviders
{
    public interface IDateProvider
    {
        public DateTime Now { get => DateTime.Now; }
    }
}
