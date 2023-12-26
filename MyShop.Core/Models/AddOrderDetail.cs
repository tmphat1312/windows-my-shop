using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyShop.Core.Models;
public class AddOrderDetail
{
    [JsonPropertyName("book")]
    public string BookId
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
