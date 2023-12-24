using System.Text.Json.Serialization;

namespace MyShop.Core.Http;

public class HttpArrayDataSchemaResponse<T>
{
    [JsonPropertyName("data")]
    public IEnumerable<T> Data
    {
        get; set;
    } = new List<T>();
}

