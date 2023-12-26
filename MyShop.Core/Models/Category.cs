using System.Text.Json.Serialization;

namespace MyShop.Core.Models;
public class Category
{
    [JsonPropertyName("id")]
    public string Id
    {
        get; set;
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get; set;
    }

    [JsonPropertyName("description")]
    public string Description
    {
        get; set;
    }

    [JsonPropertyName("created_at")]
    public string CreatedAt
    {
        get; set;
    }
}
