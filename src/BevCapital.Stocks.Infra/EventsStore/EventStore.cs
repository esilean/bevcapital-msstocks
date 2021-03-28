using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Core.EventStores;
using BevCapital.Stocks.Domain.Core.EventStores.Aggregate;
using BevCapital.Stocks.Domain.EventStores;
using BevCapital.Stocks.Domain.EventStores.Repository;
using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Infra.EventsStore
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IValidatorFactory _validationFactory;

        private readonly JsonSerializerSettings JSON_SERIALIZER_SETTINGS = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public EventStore(IEventStoreRepository eventStoreRepository,
                          IValidatorFactory validationFactory)
        {
            _eventStoreRepository = eventStoreRepository;
            _validationFactory = validationFactory;
        }

        public async Task<IEnumerable<StreamState>> GetEvents(string aggregateId, int? version = null, DateTime? createdUtc = null)
        {
            var result = await _eventStoreRepository.GetEvents(aggregateId, version, createdUtc);
            return result;
        }

        public async Task<TAggregate> AggregateStream<TAggregate>(string aggregateId, int? version = null, DateTime? createdUtc = null) where TAggregate : IAggregate
        {
            var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);

            var events = await GetEvents(aggregateId, version, createdUtc);
            var v = 0;

            foreach (var @event in events)
            {
                aggregate.InvokeIfExists("Apply", JsonConvert.DeserializeObject<IEvent>(@event?.Data, JSON_SERIALIZER_SETTINGS));
                aggregate.SetIfExists(nameof(IAggregate.Version), ++v);
                aggregate.SetIfExists(nameof(IAggregate.CreatedAtUtc), @event.CreatedAtUtc);
            }

            return aggregate;
        }

        public async Task AppendEvent<TAggregate>(string aggregateId, IEvent @event, int? expectedVersion = null, Func<StreamState, Task> action = null) where TAggregate : IAggregate
        {
            var version = 1;

            var events = await GetEvents(aggregateId);
            var versions = events.Select(e => e.Version).ToList();

            if (expectedVersion.HasValue)
            {
                if (versions.Contains(expectedVersion.Value))
                {
                    throw new Exception($"Version '{expectedVersion.Value}' already exists for stream '{aggregateId}'");
                }
            }
            else
            {
                version = versions.DefaultIfEmpty(0).Max() + 1;
            }

            var stream = new StreamState
            {
                AggregateId = aggregateId,
                Version = version,
                Type = EventTypeHelper.GetTypeName(@event.GetType()),
                Data = @event == null ? "{}" : JsonConvert.SerializeObject(@event, JSON_SERIALIZER_SETTINGS)
            };

            await _eventStoreRepository.Add(stream);

            if (action != null)
            {
                await action(stream);
            }
        }

        public async Task Store<TAggregate>(TAggregate aggregate, Func<StreamState, Task> action = null) where TAggregate : IAggregate
        {
            var events = aggregate.DequeueUncommittedEvents();

            foreach (var @event in events)
            {
                var validator = _validationFactory.GetValidator(@event.GetType());
                var result = validator?.Validate(new ValidationContext<IEvent>(@event));
                if (result != null && !result.IsValid)
                {
                    continue;
                }

                await AppendEvent<TAggregate>(aggregate.Id, @event, action: action);
            }
        }
    }
}
