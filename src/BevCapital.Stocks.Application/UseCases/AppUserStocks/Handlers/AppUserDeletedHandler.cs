using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Events.AppUserEvents;
using BevCapital.Stocks.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks.Handlers
{

    public class AppUserDeletedHandler : IEventHandler<AppUserDeletedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppUserDeletedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AppUserDeletedEvent @event, CancellationToken cancellationToken)
        {
            var appUser = await _unitOfWork.AppUsers.FindAsync(@event.UserId);
            if (appUser != null)
            {
                _unitOfWork.AppUsers.Remove(appUser);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
