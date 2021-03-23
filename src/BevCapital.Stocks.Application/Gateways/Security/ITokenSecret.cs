using System;
using System.Threading.Tasks;

namespace BevCapital.Stocks.Application.Gateways.Security
{
    public interface ITokenSecret : IDisposable
    {
        Task<string> GetSecretAsync();
    }
}
