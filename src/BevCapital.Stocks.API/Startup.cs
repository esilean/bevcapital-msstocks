using Amazon.XRay.Recorder.Core;
using Amazon.XRay.Recorder.Core.Internal.Entities;
using BevCapital.Stocks.API.Middlewares;
using BevCapital.Stocks.Data.Context;
using BevCapital.Stocks.Domain.Entities;
using BevCapital.Stocks.Infra.ServiceExtensions;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace BevCapital.Stocks.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                    .AddAppCore(Configuration)
                    .AddAppSwaggerLogon()
                    .AddAppSecurity()
                    .AddAppDistributedCache(Configuration)
                    .AddAppAWS(Configuration, HostingEnvironment)
                    .AddAppDatabase(Configuration)
                    .AddAppHealthCheck(Configuration);

            services.AddAppEventStore(Configuration)
                    .AddAppEventStoreAggregate<Stock>();

        }

        public void Configure(IApplicationBuilder app,
                              IWebHostEnvironment env,
                              StockContext stockContext,
                              EventStoreContext eventStoreContext,
                              ILogger<Startup> logger)
        {
            Log.Information($"Hosting enviroment = {env.EnvironmentName}");

            app.UseMiddleware<ErrorHandlerMiddleware>();
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerLogon();
            app.UseAWS();
            app.UseSecurity();
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = reg => reg.Tags.Contains("ready"),
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                endpoints.Map("/", async (context) =>
                {
                    var result = System.Text.Json.JsonSerializer.Serialize(new
                    {
                        machineName = Environment.MachineName,
                        appName = env.ApplicationName,
                        environment = env.EnvironmentName
                    });

                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync(result.ToString());
                });
            });

            Seed(stockContext, eventStoreContext, logger);
        }

        private void Seed(StockContext stockContext, EventStoreContext eventStoreContext, ILogger<Startup> logger)
        {
            // XRAY - EFCore - AsyncLocal Problems
            String traceId = TraceId.NewId();
            AWSXRayRecorder.Instance.BeginSegment("DB Migration", traceId);
            try
            {
                logger.LogInformation("Initializing StockContext Database Migration.");
                stockContext.Database.Migrate();
                logger.LogInformation("Finishing StockContext Database Migration...");

                logger.LogInformation("Initializing EventStoreContext Database Migration.");
                eventStoreContext.Database.Migrate();
                logger.LogInformation("Finishing EventStoreContext Database Migration...");
            }
            catch (Exception e)
            {
                AWSXRayRecorder.Instance.AddException(e);
            }
            finally
            {
                AWSXRayRecorder.Instance.EndSegment();
            }
        }
    }
}
