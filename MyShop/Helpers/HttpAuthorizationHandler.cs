using System.Net;
using MyShop.Contracts.Services;
using MyShop.Core.Contracts.Services;

namespace MyShop.Core.Http;

public class AuthenticationResponseHandler : DelegatingHandler
{
    private readonly INavigationService _navigationService;
    private readonly IAuthenticationService _authenticationService;


    public AuthenticationResponseHandler(INavigationService navigationService, IAuthenticationService authenticationService)
    {
        _navigationService = navigationService;
        _authenticationService = authenticationService;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if ((request.RequestUri.ToString().Contains("login")))
        {
            return response;
        }

        if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
        {
            await _authenticationService.LogoutAsync();
            _navigationService.Refresh();
        }

        return response;
    }
}
