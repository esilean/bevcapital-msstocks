using BevCapital.Stocks.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.Repositories
{
    public interface IAppUserRepositoryAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<AppUser> FindAsync(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        Task AddAsync(AppUser appUser);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appUser"></param>
        void Remove(AppUser appUser);
    }
}
