using MediatR;

namespace BevCapital.Stocks.Domain.Core.Events
{
    public interface IEventHandler<T> : INotificationHandler<T> where T : IEvent
    { }
}
