﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class HealthExtensions
    {
        public static IServiceCollection ConfigureHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddMySql(
                        connectionString: configuration.GetConnectionString("SqlCNN"),
                        name: "DB - RDS/MySqlServer",
                        timeout: TimeSpan.FromSeconds(60),
                        tags: new string[] { "ready" },
                        failureStatus: HealthStatus.Unhealthy);

            return services;
        }
    }
}
