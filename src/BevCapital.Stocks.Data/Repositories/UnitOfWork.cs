using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Domain.Repositories;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StocksContext _stocksContext;

        public UnitOfWork(StocksContext stocksContext)
        {
            _stocksContext = stocksContext;
            Stocks = new StockRepositoryAsync(_stocksContext);
        }

        public IStockRepositoryAsync Stocks { get; private set; }

        public async ValueTask DisposeAsync()
        {
            await _stocksContext.DisposeAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return await _stocksContext.SaveChangesAsync() > 0;
        }
    }
}
