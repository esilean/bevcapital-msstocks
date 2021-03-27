namespace BevCapital.Stocks.Application.Gateways.Security
{
    public interface IAppUserAccessor
    {
        string GetCurrentId();
        string GetCurrentEmail();
    }
}
