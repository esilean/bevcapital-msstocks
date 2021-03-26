using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Data.Repositories;
using BevCapital.Stocks.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("SqlCNN");

            var rdsEndpoint = Environment.GetEnvironmentVariable("RDS_ENDPOINT");
            var rdsPassword = Environment.GetEnvironmentVariable("RDS_PASSWORD");
            connString.Replace("RDS_ENDPOINT", rdsEndpoint);
            connString.Replace("RDS_PASSWORD", rdsPassword);

            services.AddDbContext<StocksContext>(opts =>
            {
                opts.UseMySql(connString);
                opts.AddXRayInterceptor(true);
            });

            services.AddScoped<IStockRepositoryAsync, StockRepositoryAsync>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<StocksContext>();

            return services;
        }
    }
}
