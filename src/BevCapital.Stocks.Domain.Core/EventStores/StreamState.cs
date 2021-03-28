using System;

namespace BevCapital.Stocks.Domain.Core.EventStores
{
    public class StreamState
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string AggregateId { get; set; }
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
        public string Type { get; set; }
        public string Data { get; set; }
        public int Version { get; set; } = 0;

    }
}
