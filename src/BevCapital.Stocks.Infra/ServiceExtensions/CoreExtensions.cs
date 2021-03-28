using AutoMapper;
using BevCapital.Stocks.Application.DataProviders;
using BevCapital.Stocks.Application.UseCases.Stocks;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Infra.DataProviders;
using BevCapital.Stocks.Infra.Filters;
using BevCapital.Stocks.Infra.Notifications;
using BevCapital.Stocks.Infra.Security.Tokens;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class CoreExtensions
    {
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

            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

            return services;
        }
    }
}
