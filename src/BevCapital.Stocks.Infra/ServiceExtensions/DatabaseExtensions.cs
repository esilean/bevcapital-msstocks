using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Data.Repositories;
using BevCapital.Stocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StocksContext>(opts =>
            {
                opts.UseMySql(configuration.GetConnectionString("SqlCNN"));
                opts.AddXRayInterceptor(true);
            });

            services.AddScoped<IStockRepositoryAsync, StockRepositoryAsync>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<StocksContext>();

            return services;
        }
    }
}
