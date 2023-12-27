using System.Text.Json.Serialization;

namespace MyShop.Core.Models;

public class OrderDetail
{
    [JsonPropertyName("id")]
    public string OrderId
    {
        get; set;
    }

    [JsonPropertyName("book")]
    public Book Book
    {
    
           get; set;
     }

    [JsonPropertyName("quantity")]
    public int Quantity
    {
        get; set;
    }

    [JsonPropertyName("price")]
    public double Price
    {
        get; set;
    }
}
