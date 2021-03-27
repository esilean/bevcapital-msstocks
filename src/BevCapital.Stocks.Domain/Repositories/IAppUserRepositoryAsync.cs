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
    }
}
