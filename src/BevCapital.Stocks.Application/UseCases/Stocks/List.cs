using AutoMapper;
using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using BevCapital.Stocks.Domain.Constants;
using BevCapital.Stocks.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.UseCases.Stocks
{
    public class ListStock
    {
        public class Query : IRequest<List<StockOut>> { }

        public class Handler : IRequestHandler<Query, List<StockOut>>
        {

            private readonly IUnitOfWork _unitOfWork;
            private readonly IMapper _mapper;
            private readonly IDistributedCache _distributedCache;

            public Handler(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
            {
                _unitOfWork = unitOfWork;
                _mapper = mapper;
                _distributedCache = distributedCache;
            }

            public async Task<List<StockOut>> Handle(Query request, CancellationToken cancellationToken)
            {
                var cachedStocks = await _distributedCache.GetAsync<List<StockOut>>(CacheKeys.LIST_ALL_STOCKS);
                if (cachedStocks == null || !cachedStocks.Any())
                {
                    var stocks = await _unitOfWork.Stocks.GetAllAsync();
                    cachedStocks = _mapper.Map<List<StockOut>>(stocks);

                    await _distributedCache.SetAsync<List<StockOut>>
                                                    (
                                                        CacheKeys.LIST_ALL_STOCKS,
                                                        cachedStocks,
                                                        new DistributedCacheEntryOptions
                                                        {
                                                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                                                        }
                                                    );
                }

                return cachedStocks;
            }
        }
    }
}
