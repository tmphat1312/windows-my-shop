using System.Text;
using System.Text.Json;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Http;

namespace MyShop.Core.Services;

public class AuthenticationService : IAuthenticationService
{
    public string AccessToken
    {
        get; set;
    }

    public string GetAccessToken() => AccessToken;
    public bool IsAuthenticated() => !string.IsNullOrEmpty(AccessToken);

    private readonly IHttpClientFactory _httpClientFactory;

    public AuthenticationService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(string, int)> LoginAsync(string email, string password)
    {
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var httpResponse = await client.PostAsync("auth/login", new StringContent(JsonSerializer.Serialize(new
            {
                email,
                password
            }), Encoding.UTF8, "application/json"));

            var responseContent = await httpResponse.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<HttpLoginSchemaResponse>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });


            if (httpResponse.IsSuccessStatusCode)
            {
                AccessToken = loginResponse.AccessToken;

                ERROR_CODE = 0;
            }
            else
            {
                ERROR_CODE = (int)httpResponse.StatusCode;

                if (ERROR_CODE == 400)
                {
                    message = loginResponse.Error.Message;
                }
                else if (ERROR_CODE == 401)
                {
                    message = loginResponse.Message;
                }
                else
                {
                    message = "Something went wrong";
                    ERROR_CODE = 1;
                }
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = 1;
        }

        return (message, ERROR_CODE);
    }

    public Task<bool> LogoutAsync() => throw new NotImplementedException();
}
