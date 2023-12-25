using Newtonsoft.Json;

namespace MyShop.Core.Models;
public class User
{
    [JsonProperty("_id")]
    public string ID
    {
        get; set;
    }

    [JsonProperty("name")]
    public string Name
    {
        get; set;
    } = "";


    [JsonProperty("email")]
    public string Email
    {
        get; set;
    }


    public string Password
    {
        get; set;

    } = "";

    [JsonProperty("role")]
    public string Role
    {

        get; set;
    } = "customer";

    [JsonProperty("image")]

    public string Image { get; set; } = "noimage";

    public byte[] ImageBytes { get; set; } = null;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt
    {
        get; set;
    }

    //public DateTime PasswordUpdateAt
    //{
    //    get; set;
    //}

}
