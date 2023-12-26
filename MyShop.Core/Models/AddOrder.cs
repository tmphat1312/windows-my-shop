using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyShop.Core.Models;
public class AddOrder
{
    [JsonPropertyName("user")]
    public string UserId
    {
         get; set;
    }

    [JsonPropertyName("orderDetails")]
    public ICollection<AddOrderDetail> OrderDetails
    {
         get; set;
    }
}
