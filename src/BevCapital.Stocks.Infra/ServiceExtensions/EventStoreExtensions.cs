using BevCapital.Stocks.Application.Gateways.EventsStore;
using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Data.EventStores;
using BevCapital.Stocks.Domain.Core.EventStores.Aggregate;
using BevCapital.Stocks.Domain.EventStores;
using BevCapital.Stocks.Domain.EventStores.Repository;
using BevCapital.Stocks.Infra.EventsStore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class EventStoreExtensions
    {
        public static IServiceCollection AddAppEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var connString = configuration.GetConnectionString("SqlCNN");
            var rdsEndpoint = Environment.GetEnvironmentVariable("RDS_ENDPOINT");
            var rdsPassword = Environment.GetEnvironmentVariable("RDS_PASSWORD");
            connString.Replace("RDS_ENDPOINT", rdsEndpoint);
            connString.Replace("RDS_PASSWORD", rdsPassword);

            services.AddDbContext<EventStoreContext>(opts =>
            {
                opts.UseMySql(connString);
                opts.AddXRayInterceptor(true);
            });
            services.AddScoped<EventStoreContext>();

            services.AddScoped<IEventStore, EventStore>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();

            return services;
        }

        public static IServiceCollection AddAppEventStoreAggregate<TAggregate>(this IServiceCollection services) where TAggregate : IAggregate
        {
            services.AddScoped<IEventStoreApp<TAggregate>, EventStoreApp<TAggregate>>();
            return services;
        }
    }
}
