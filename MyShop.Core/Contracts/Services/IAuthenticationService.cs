namespace MyShop.Core.Contracts.Services;
public interface IAuthenticationService
{
    public string GetAccessToken();
    public bool IsAuthenticated();
    public Task<(string, int)> LoginAsync(string email, string password);
    public Task<bool> LogoutAsync();
}
