using BevCapital.Stocks.Application.Gateways.Security;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace BevCapital.Stocks.Infra.Security.AppUser
{
    public class AppUserAccessor : IAppUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentId()
        {
            var username = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                    x.Type == ClaimTypes.NameIdentifier)?.Value;

            return username;
        }

        public string GetCurrentEmail()
        {
            var email = _httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x =>
                    x.Type == ClaimTypes.Email)?.Value;

            return email;
        }
    }
}
