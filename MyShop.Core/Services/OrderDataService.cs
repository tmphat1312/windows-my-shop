using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class OrderDataService
{
    private List<Order> _orders;

    private static IEnumerable<Order> Orders()
    {
        var sampleOrders = new List<Order>()
           {
                new(),
                new(),
                new(),
                new(),
                new(),
            };

        return sampleOrders;
    }
}
