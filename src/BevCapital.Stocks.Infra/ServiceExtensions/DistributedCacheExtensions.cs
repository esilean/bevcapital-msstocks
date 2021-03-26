﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class DistributedCacheExtensions
    {
        public static IServiceCollection ConfigureDistributedCache(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheCNN = configuration.GetConnectionString("CacheCNN");
            if (string.IsNullOrWhiteSpace(cacheCNN))
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                var cacheEndpoint = Environment.GetEnvironmentVariable("CACHE_ENDPOINT");
                var cachePassword = Environment.GetEnvironmentVariable("CACHE_PASSWORD");
                cacheCNN.Replace("CACHE_ENDPOINT", cacheEndpoint);
                cacheCNN.Replace("CACHE_PASSWORD", cachePassword);
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheCNN;
                    options.InstanceName = "Stocks:";
                });
            }

            return services;
        }
    }
}
