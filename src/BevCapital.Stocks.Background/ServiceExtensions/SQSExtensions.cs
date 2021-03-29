using Amazon.SQS;
using BevCapital.Stocks.Background.Aws;
using BevCapital.Stocks.Background.Aws.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BevCapital.Stocks.Background.ServiceExtensions
{

    public static class SQSExtensions
    {
        public static IServiceCollection AddAppSQSService(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.Configure<SQSSettings>(configuration.GetSection("SQSSettings"));

            services.AddSingleton<IAwsSQS, AwsSQS>();

            services.AddAWSService<IAmazonSQS>();
            services.AddHostedService<SQSBackgroundService>();

            return services;
        }
    }
}
