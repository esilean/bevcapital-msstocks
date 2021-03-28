using BevCapital.Stocks.Domain.Core.EventStores.Aggregate;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.Gateways.EventsStore
{
    public interface IEventStoreApp<TAggregate> where TAggregate : IAggregate
    {
        Task<TAggregate> Find(string id);
        Task Add(TAggregate aggregate);
    }
}
