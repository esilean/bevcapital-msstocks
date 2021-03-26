using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Data.Repositories
{

    public class StockRepositoryAsync : IStockRepositoryAsync
    {
        private readonly StocksContext _stocksContext;

        public StockRepositoryAsync(StocksContext stocksContext)
        {
            _stocksContext = stocksContext;
        }

        public async Task<IEnumerable<Stock>> GetAllAsync()
        {
            return await _stocksContext.Stocks.Include(x => x.StockPrice).ToListAsync();
        }

        public async Task<Stock> FindAsync(string symbol)
        {
            return await _stocksContext.Stocks.Include(x => x.StockPrice)
                                              .FirstOrDefaultAsync(x => x.Symbol == symbol);
        }

        public async Task AddAsync(Stock stock)
        {
            await _stocksContext.Stocks.AddAsync(stock);
        }

        public void Remove(Stock stock)
        {
            _stocksContext.Stocks.Remove(stock);
        }
    }
}
