using System.Text.Json.Serialization;

namespace MyShop.Core.Http;

public class HttpLoginSchemaResponse
{
    [JsonPropertyName("accessToken")]
    public string AccessToken
    {
        get; set;
    }

    [JsonPropertyName("userId")]
    public string UserId
    {
        get; set;
    }

    [JsonPropertyName("message")]
    public string Message
    {
        get; set;
    }

    [JsonPropertyName("error")]
    public Error Error
    {
        get; set;
    }
}
