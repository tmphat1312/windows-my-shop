using System.Text.Json.Serialization;

namespace MyShop.Core.Models;

public class Book
{
    [JsonPropertyName("id")]
    public String Id
    {
        get; set;
    }

    [JsonPropertyName("name")]
    public string Name
    {
        get; set;
    }

    [JsonPropertyName("image")]
    public string Image
    {
        get; set;
    } = "https://via.placeholder.com/300";

    [JsonPropertyName("purchasePrice")]
    public double PurchasePrice
    {
        get; set;
    }

    [JsonPropertyName("sellingPrice")]
    public double SellingPrice
    {
        get; set;
    }

    [JsonPropertyName("author")]
    public string Author
    {

        get; set;
    }

    [JsonPropertyName("publishedYear")]
    public int PublishedYear
    {
        get; set;
    }

    [JsonPropertyName("ratingsAverage")]
    public double RatingsAverage
    {
        get; set;
    }

    [JsonPropertyName("quantity")]
    public int Quantity
    {
        get; set;
    }

    public int OrderQuantity
    {
        get; set;
    } = 0;

    [JsonPropertyName("description")]
    public string Description
    {
        get; set;
    }

    [JsonPropertyName("categoryId")]
    public string CategoryId
    {
        get; set;
    }

    [JsonPropertyName("category")]
    public Category Category
    {
        get; set;
    }

    public byte[] ImageBytes
    {
        get; set;
    }
}
