using AutoMapper;
using BevCapital.Stocks.Application.DataProviders;
using BevCapital.Stocks.Application.UseCases.Stocks;
using BevCapital.Stocks.Domain.Core.Events;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Infra.DataProviders;
using BevCapital.Stocks.Infra.Events;
using BevCapital.Stocks.Infra.Filters;
using BevCapital.Stocks.Infra.Notifications;
using BevCapital.Stocks.Infra.Security.Tokens;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class CoreExtensions
    {
        public static void AddSecrets(this IServiceCollection _, IConfiguration configuration)
        {
            var secretsJson = File.ReadAllText(@"appsecrets.json");
            var secrets = JsonConvert.DeserializeObject<IDictionary<string, string>>(secretsJson);

            foreach (var secret in secrets)
            {
                var values = secret.Value.Split("::");
                foreach (var value in values)
                {
                    configuration[secret.Key] = configuration[secret.Key]?.Replace(value, Environment.GetEnvironmentVariable(value));
                }
            }
        }

        public static IServiceCollection AddAppCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(opts =>
            {
                opts.Filters.Add<NotificationFilter>();
            })
            .AddNewtonsoftJson(opts =>
            {
                opts.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
            })
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<Create>();
            });


            services.AddMediatR(typeof(Create.Handler).Assembly);
            services.AddAutoMapper(typeof(Create.Handler).Assembly);

            services.AddScoped<IAppNotificationHandler, AppNotificationHandler>();
            services.AddScoped<IDateProvider, DateProvider>();

            services.AddScoped<IEventBus, EventBus>();

            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

            return services;
        }
    }
}
