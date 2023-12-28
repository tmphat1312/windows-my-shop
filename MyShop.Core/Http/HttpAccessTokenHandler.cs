using System.Net.Http.Headers;
using MyShop.Core.Contracts.Services;

namespace MyShop.Core.Http;

public class AccessTokenHandler : DelegatingHandler
{
    private readonly IAuthenticationService _authenticationService;

    public AccessTokenHandler(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Get the access token from the authentication service
        var accessToken = _authenticationService.GetAccessToken();

        if (!string.IsNullOrEmpty(accessToken))
        {
            // Add the access token to the request headers
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        // Pass the request to the base handler
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }
}
