using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyShop.Core.Models;
public class User
{
    [JsonPropertyName("_id")]
    public string ID
    {
        get; set;
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get; set;
    } = "";


    [JsonPropertyName("email")]
    public string Email
    {
        get; set;
    }


    public string Password
    {
        get; set;

    } = "";

    [JsonPropertyName("role")]
    public string Role
    {

        get; set;
    } = "customer";

    [JsonPropertyName("image")]

    public string Image { get; set; } = "noimage";

    public byte[] ImageBytes { get; set; } = null;

    [JsonPropertyName("createdAt")]
    public DateTime CreatedAt
    {
        get; set;
    }

    //public DateTime PasswordUpdateAt
    //{
    //    get; set;
    //}

}
