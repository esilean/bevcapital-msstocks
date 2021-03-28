using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Data.Repositories
{

    public class AppUserRepositoryAsync : IAppUserRepositoryAsync
    {
        private readonly StockContext _stocksContext;

        public AppUserRepositoryAsync(StockContext stocksContext)
        {
            _stocksContext = stocksContext;
        }

        public async Task<AppUser> FindAsync(Guid id)
        {
            return await _stocksContext.AppUsers.FindAsync(id);
        }
    }
}
