using BevCapital.Stocks.Application.Errors;
using BevCapital.Stocks.Application.Gateways.Security;
using BevCapital.Stocks.Domain.Constants;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks
{
    public class Add
    {
        public class AddAppUserStockCommand : IRequest
        {
            public string Symbol { get; set; }
        }

        public class CommandValidator : AbstractValidator<AddAppUserStockCommand>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Symbol).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<AddAppUserStockCommand>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IAppUserAccessor _appUserAccessor;
            private readonly IAppNotificationHandler _appNotificationHandler;
            private readonly IDistributedCache _distributedCache;

            public Handler(IUnitOfWork unitOfWork,
                           IAppUserAccessor appUserAccessor,
                           IAppNotificationHandler appNotificationHandler,
                           IDistributedCache distributedCache)
            {
                _unitOfWork = unitOfWork;
                _appUserAccessor = appUserAccessor;
                _appNotificationHandler = appNotificationHandler;
                _distributedCache = distributedCache;
            }

            public async Task<Unit> Handle(AddAppUserStockCommand request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(_appUserAccessor.GetCurrentId(), out Guid appUserId))
                {
                    _appNotificationHandler.AddNotification(Keys.APPUSERSTOCK, Messages.APPUSER_NOT_FOUND);
                    return Unit.Value;
                }

                var appUser = await _unitOfWork.AppUsers.FindAsync(appUserId);
                if (appUser == null)
                {
                    _appNotificationHandler.AddNotification(Keys.APPUSERSTOCK, Messages.APPUSER_NOT_FOUND);
                    return Unit.Value;
                }

                var stock = await _unitOfWork.Stocks.FindAsync(request.Symbol);
                if (stock == null)
                {
                    _appNotificationHandler.AddNotification(Keys.APPUSERSTOCK, Messages.STOCK_NOT_FOUND);
                    return Unit.Value;
                }

                var appUserStock = await _unitOfWork.AppUserStocks.FindAsync(appUserId, request.Symbol);
                if (appUserStock != null)
                {
                    _appNotificationHandler.AddNotification(Keys.APPUSERSTOCK, Messages.APPUSERSTOCK_EXISTS);
                    return Unit.Value;
                }

                appUserStock = new AppUserStock(appUserId, request.Symbol);
                await _unitOfWork.AppUserStocks.AddAsync(appUserStock);

                var success = await _unitOfWork.SaveAsync();
                if (success)
                {
                    var cacheKey = string.Format(CacheKeys.LIST_ALL_APPUSERSTOCKS_USERIDPARAM, appUserId);
                    await _distributedCache.RemoveAsync(cacheKey, cancellationToken);

                    return Unit.Value;
                }



                throw new AppException(HttpStatusCode.InternalServerError);
            }
        }
    }
}
