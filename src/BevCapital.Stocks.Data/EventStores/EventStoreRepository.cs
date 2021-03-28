using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Domain.Core.EventStores;
using BevCapital.Stocks.Domain.EventStores;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Data.EventStores
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly EventStoreContext _eventStoreContext;

        public EventStoreRepository(EventStoreContext eventStoreContext)
        {
            _eventStoreContext = eventStoreContext;
        }

        public async Task Add(StreamState streamState)
        {
            await _eventStoreContext.Streams.AddAsync(streamState);
            await _eventStoreContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<StreamState>> GetEvents(string aggregateId, int? version = null, DateTime? createdAtUtc = null)
        {
            var query = _eventStoreContext.Streams
                        .Where(x => x.AggregateId == aggregateId)
                        .AsQueryable();

            if (version.HasValue)
            {
                query.Where(q => q.Version == version);
            }
            if (createdAtUtc.HasValue)
            {
                query.Where(q => q.CreatedAtUtc == createdAtUtc);
            }

            var result = await query.AsNoTracking().ToListAsync();
            return result;
        }
    }
}
