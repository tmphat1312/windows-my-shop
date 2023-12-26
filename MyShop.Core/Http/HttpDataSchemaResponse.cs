using System.Text.Json.Serialization;

namespace MyShop.Core.Http;

public class HttpDataSchemaResponse<T>
{
    [JsonPropertyName("data")]
    public T Data
    {
        get; set;
    }
}

