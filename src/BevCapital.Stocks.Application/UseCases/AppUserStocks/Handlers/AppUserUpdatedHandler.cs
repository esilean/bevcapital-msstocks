using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Events.AppUserEvents;
using BevCapital.Stocks.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks.Handlers
{

    public class AppUserUpdatedHandler : IEventHandler<AppUserUpdatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppUserUpdatedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AppUserUpdatedEvent @event, CancellationToken cancellationToken)
        {
            var appUser = await _unitOfWork.AppUsers.FindAsync(@event.UserId);
            if (appUser != null)
            {
                appUser.Update(@event.Name);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
