using BevCapital.Stocks.Domain.Core.EventStores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.EventStores
{
    public interface IEventStoreRepository
    {
        Task Add(StreamState streamState);
        Task<IEnumerable<StreamState>> GetEvents(string aggregateId, int? version = null, DateTime? createdAtUtc = null);
    }
}
