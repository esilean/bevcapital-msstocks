using BevCapital.Stocks.Application.Gateways.EventsStore;
using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Core.EventStores;
using BevCapital.Stocks.Domain.Core.EventStores.Aggregate;
using BevCapital.Stocks.Domain.EventStores.Repository;
using System;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Infra.EventsStore
{

    public class EventStoreApp<TAggregate> : IEventStoreApp<TAggregate> where TAggregate : IAggregate
    {
        private readonly IEventStore _eventStore;
        //private readonly IEventBus _eventBus;

        public EventStoreApp(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public virtual async Task<TAggregate> Find(string id)
        {
            var result = await _eventStore.AggregateStream<TAggregate>(id);
            return result;
        }

        public virtual async Task Add(TAggregate aggregate)
        {
            await _eventStore.Store(aggregate, PublishEvent);
        }

        private async Task PublishEvent(StreamState stream)
        {
            if (stream is null)
            {
                throw new Exception($"{nameof(stream)} was null");
            }

            // TODO
            //await _eventBus.Send();

            await Task.CompletedTask;
        }
    }
}
