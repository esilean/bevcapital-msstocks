using AutoMapper;
using BevCapital.Stocks.Application.Gateways.Security;
using BevCapital.Stocks.Application.UseCases.AppUserStocks.Response;
using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using BevCapital.Stocks.Domain.Constants;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks
{
    public class ListStock
    {
        public class ListAppUserStockQuery : IRequest<AppUserStockOut> { }

        public class Handler : IRequestHandler<ListAppUserStockQuery, AppUserStockOut>
        {

            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;
            private readonly IAppUserAccessor _appUserAccessor;
            private readonly IAppNotificationHandler _appNotificationHandler;

            public Handler(IUnitOfWork unitOfWork,
                           IMapper mapper,
                           IDistributedCache distributedCache,
                           IAppUserAccessor appUserAccessor,
                           IAppNotificationHandler appNotificationHandler)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _distributedCache = distributedCache;
                _appUserAccessor = appUserAccessor;
                _appNotificationHandler = appNotificationHandler;
            }

            public async Task<AppUserStockOut> Handle(ListAppUserStockQuery request, CancellationToken cancellationToken)
            {
                if (!Guid.TryParse(_appUserAccessor.GetCurrentId(), out Guid appUserId))
                {
                    _appNotificationHandler.AddNotification(Keys.APPUSERSTOCK, Messages.APPUSER_NOT_FOUND);
                    return null;
                }

                var appUser = await _unitOfWork.AppUsers.FindAsync(appUserId);
                if (appUser == null)
                {
                    _appNotificationHandler.AddNotification(Keys.APPUSERSTOCK, Messages.APPUSER_NOT_FOUND);
                    return null;
                }

                var cacheKey = string.Format(CacheKeys.LIST_ALL_APPUSERSTOCKS_USERIDPARAM, appUserId);
                var cachedAppUserStocks = await _distributedCache.GetAsync<AppUserStockOut>(cacheKey, cancellationToken);
                if (cachedAppUserStocks == null)
                {
                    var appUserStocks = await _unitOfWork.AppUserStocks.GetAllStocksFromUser(appUserId);

                    cachedAppUserStocks = new AppUserStockOut
                    {
                        Id = appUser.Id,
                        Name = appUser.Name,
                        StockOuts = _mapper.Map<List<StockOut>>(appUserStocks.Select(x => x.Stock))
                    };

                    await _distributedCache.SetAsync<AppUserStockOut>
                                                    (
                                                        cacheKey,
                                                        cachedAppUserStocks,
                                                        new DistributedCacheEntryOptions
                                                        {
                                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(15),
                                                            AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(15)
                                                        },
                                                        cancellationToken
                                                    );
                }

                return cachedAppUserStocks;
            }
        }
    }
}
