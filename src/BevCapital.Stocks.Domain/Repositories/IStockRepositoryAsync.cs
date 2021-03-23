using BevCapital.Stocks.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.Repositories
{
    public interface IStockRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Stock>> GetAllAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<Stock> FindAsync(string symbol);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        Task AddAsync(Stock stock);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        void Update(Stock stock);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stock"></param>
        void Remove(Stock stock);
    }
}
