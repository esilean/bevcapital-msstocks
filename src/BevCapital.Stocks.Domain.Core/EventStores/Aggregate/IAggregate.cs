using BevCapital.Stocks.Domain.Core.Events;
using System;
using System.Collections.Generic;

namespace BevCapital.Stocks.Domain.Core.EventStores.Aggregate
{
    public interface IAggregate
    {
        string Id { get; }
        int Version { get; }
        DateTime CreatedAtUtc { get; }
        DateTime UpdatedAtUtc { get; }

        IEnumerable<IEvent> DequeueUncommittedEvents();
    }
}
