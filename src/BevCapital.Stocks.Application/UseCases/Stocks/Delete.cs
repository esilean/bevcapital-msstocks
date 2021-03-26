using BevCapital.Stocks.Application.Errors;
using BevCapital.Stocks.Domain.Constants;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.Stocks
{
    public class Delete
    {
        public class Command : IRequest
        {
            public string Symbol { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Symbol).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAppNotificationHandler _appNotificationHandler;
            private readonly IDistributedCache _distributedCache;

            public Handler(IUnitOfWork unitOfWork,
                           IAppNotificationHandler appNotificationHandler,
                           IDistributedCache distributedCache)
            {
                _unitOfWork = unitOfWork;
                _appNotificationHandler = appNotificationHandler;
                _distributedCache = distributedCache;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var stock = await _unitOfWork.Stocks.FindAsync(request.Symbol);
                if (stock == null)
                {
                    _appNotificationHandler.AddNotification(Keys.APPSTOCK, Messages.STOCK_NOT_FOUND);
                    return Unit.Value;
                }

                _unitOfWork.Stocks.Remove(stock);
                var success = await _unitOfWork.SaveAsync();
                if (success)
                {
                    await _distributedCache.RemoveAsync(CacheKeys.LIST_ALL_STOCKS, cancellationToken);

                    return Unit.Value;
                }

                throw new AppException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
