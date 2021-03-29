using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Domain.Events.AppUserEvents;
using BevCapital.Stocks.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks.Handlers
{

    public class AppUserCreatedHandler : IEventHandler<AppUserCreatedEvent>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppUserCreatedHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AppUserCreatedEvent @event, CancellationToken cancellationToken)
        {
            var appUser = await _unitOfWork.AppUsers.FindAsync(@event.UserId);
            if (appUser == null)
            {
                appUser = AppUser.Create(@event.UserId, @event.Name, @event.Email);

                await _unitOfWork.AppUsers.AddAsync(appUser);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
