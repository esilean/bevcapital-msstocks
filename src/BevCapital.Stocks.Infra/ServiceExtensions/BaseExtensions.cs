using AutoMapper;
using BevCapital.Stocks.Application.DataProviders;
using BevCapital.Stocks.Application.UseCases.Stocks;
using BevCapital.Stocks.Domain.Notifications;
using BevCapital.Stocks.Infra.DataProviders;
using BevCapital.Stocks.Infra.Notifications;
using BevCapital.Stocks.Infra.Security.Tokens;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class BaseExtensions
    {
        public static IServiceCollection ConfigureCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(typeof(Create.Handler).Assembly);
            services.AddAutoMapper(typeof(Create.Handler).Assembly);

            services.AddScoped<IAppNotificationHandler, AppNotificationHandler>();
            services.AddScoped<IDateProvider, DateProvider>();

            services.Configure<TokenSettings>(configuration.GetSection("TokenSettings"));

            return services;
        }
    }
}
