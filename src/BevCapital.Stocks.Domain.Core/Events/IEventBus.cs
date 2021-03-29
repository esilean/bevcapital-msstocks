using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.Core.Events
{
    public interface IEventBus
    {
        Task PublishLocal(params IEvent[] events);
    }
}
