using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Data.Repositories
{
    public class AppUserStockRepositoryAsync : IAppUserStockRepositoryAsync
    {
        private readonly StockContext _stocksContext;

        public AppUserStockRepositoryAsync(StockContext stocksContext)
        {
            _stocksContext = stocksContext;
        }

        public async Task<AppUserStock> FindAsync(Guid appUserId, string symbol)
        {
            return await _stocksContext.AppUserStocks.FindAsync(appUserId, symbol);
        }

        public async Task AddAsync(AppUserStock appUserStock)
        {
            await _stocksContext.AppUserStocks.AddAsync(appUserStock);
        }

        public void Remove(AppUserStock appUserStock)
        {
            _stocksContext.AppUserStocks.Remove(appUserStock);
        }

        public async Task<List<AppUserStock>> GetAllStocksFromUser(Guid appUserId)
        {
            return await _stocksContext.AppUserStocks
                                       .Include(x => x.AppUser)
                                       .Include(x => x.Stock).ThenInclude(x => x.StockPrice)
                                       .Where(x => x.AppUserId == appUserId)
                                       .ToListAsync();
        }
    }
}
