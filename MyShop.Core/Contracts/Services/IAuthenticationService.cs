namespace MyShop.Core.Contracts.Services;
public interface IAuthenticationService
{
    public string GetAccessToken();
    public string GetUserId();
    public bool IsAuthenticated();
    public Task<(string, int)> LoginAsync(string email, string password);
    public Task<bool> LogoutAsync();
}
