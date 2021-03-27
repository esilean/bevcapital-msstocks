using BevCapital.Stocks.Application.Gateways.Security;
using BevCapital.Stocks.Infra.Security.AppUser;
using BevCapital.Stocks.Infra.Security.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace BevCapital.Stocks.Infra.ServiceExtensions
{
    public static class SecurityExtensions
    {
        public static IServiceCollection ConfigureSecurity(this IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithExposedHeaders("WWW-Authenticate")
                    .AllowCredentials();
                });
            });

            services.AddScoped<IAppUserAccessor, AppUserAccessor>();
            services.AddSingleton<ITokenSecret, TokenSecret>();

            services.ConfigureJwt();

            return services;
        }

        public static IApplicationBuilder UseSecurity(this IApplicationBuilder builder)
        {
            builder.UseXContentTypeOptions();
            builder.UseReferrerPolicy(opt => opt.NoReferrer());
            builder.UseXXssProtection(opt => opt.EnabledWithBlockMode());
            builder.UseXfo(opt => opt.Deny());

            builder.UseCors("CorsPolicy");

            return builder;
        }

        private static void ConfigureJwt(this IServiceCollection services)
        {
            ITokenSecret tokenSecret = services.BuildServiceProvider().GetRequiredService<ITokenSecret>();
            var tokenKey = tokenSecret.GetSecretAsync().Result;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
