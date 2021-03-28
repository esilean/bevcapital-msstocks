using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Core.EventStores;
using BevCapital.Stocks.Domain.Core.EventStores.Aggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.EventStores.Repository
{
    public interface IEventStore
    {
        //void AddSnapshot(ISnapshot snapshot);

        //void AddProjection(IProjection projection);

        Task<IEnumerable<StreamState>> GetEvents(string aggregateId, int? version = null, DateTime? createdUtc = null);

        Task<TAggregate> AggregateStream<TAggregate>(string aggregateId, int? version = null, DateTime? createdUtc = null) where TAggregate : IAggregate;

        Task AppendEvent<TAggregate>(string aggregateId, IEvent @event, int? expectedVersion = null, Func<StreamState, Task> action = null) where TAggregate : IAggregate;

        Task Store<TAggregate>(TAggregate aggregate, Func<StreamState, Task> action = null) where TAggregate : IAggregate;

    }
}
