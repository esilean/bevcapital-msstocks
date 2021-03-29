using BevCapital.Stocks.Domain.Core.Events;
using MediatR;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Infra.Events
{
    public class EventBus : IEventBus
    {
        private readonly IMediator _mediator;

        public EventBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual async Task PublishLocal(params IEvent[] events)
        {
            foreach (var @event in events)
            {
                await _mediator.Publish(@event);
            }
        }
    }
}
