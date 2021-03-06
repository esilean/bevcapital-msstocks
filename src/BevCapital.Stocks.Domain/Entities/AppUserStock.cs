using System;

namespace BevCapital.Stocks.Domain.Entities
{
    public class AppUserStock
    {
        public Guid AppUserId { get; private set; }

        public virtual AppUser AppUser { get; private  set; }

        public string StockId { get; private set; }

        public virtual Stock Stock { get; private set; }

        public AppUserStock(Guid appUserId, string stockId)
        {
            AppUserId = appUserId;
            StockId = stockId;
        }
    }
}
