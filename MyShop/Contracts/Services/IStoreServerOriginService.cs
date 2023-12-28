namespace MyShop.Contracts.Services;
public interface IStoreServerOriginService
{
    public string Host
    {
        get;
    }
    public int Port
    {
        get;
    }

    Task<bool> SaveServerOriginAsync(string host, int port);
    Task<(string Host, int Port)> TryGetServerOriginAsync();
}
