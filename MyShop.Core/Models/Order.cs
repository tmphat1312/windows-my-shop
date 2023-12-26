using System.Text.Json.Serialization;

namespace MyShop.Core.Models;

public class Order
{
    [JsonPropertyName("id")]
    public string Id
    {
        get; set;
    }

    [JsonPropertyName("user")]
    public User User
    {
    
           get; set;
    }

    [JsonPropertyName("orderDate")]
    public DateTime OrderDate
    {

        get; set;
    }

    [JsonPropertyName("status")]
    public string Status
    {
        get; set;
    }

    [JsonPropertyName("description")]
    public string Description
    {
        get; set;
    }

    [JsonPropertyName("totalPrice")]
    public double TotalPrice
    {
        get; set;
    }

    [JsonPropertyName("finalPrice")]
    public double FinalPrice
    {
        get; set;
    }

    [JsonPropertyName("orderDetails")]
    public ICollection<OrderDetail> Details
    {
        get; set;
    }

    
}
