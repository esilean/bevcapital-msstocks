﻿using System;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Domain.Repositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IStockRepositoryAsync Stocks { get; }
        IAppUserRepositoryAsync AppUsers { get; }
        IAppUserStockRepositoryAsync AppUserStocks { get; }

        Task<bool> SaveAsync();
    }
}
