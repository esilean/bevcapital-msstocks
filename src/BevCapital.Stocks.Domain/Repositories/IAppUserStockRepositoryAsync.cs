using BevCapital.Stocks.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.Repositories
{
    public interface IAppUserStockRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUserId"></param>
        /// <param name="symbol"></param>
        /// <returns></returns>
        Task<AppUserStock> FindAsync(Guid appUserId, string symbol);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUserId"></param>
        /// <returns></returns>
        Task<List<AppUserStock>> GetAllStocksFromUser(Guid appUserId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUserStock"></param>
        /// <returns></returns>
        Task AddAsync(AppUserStock appUserStock);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUserStock"></param>
        void Remove(AppUserStock appUserStock);
    }
}
