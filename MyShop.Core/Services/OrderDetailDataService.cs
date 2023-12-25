using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class OrderDetailDataService
{
    private string _orderId;
    private List<OrderDetail> _orderDetails;

    private static IEnumerable<OrderDetail> OrderDetails()
    {
        var sampleOrderDetails = new List<OrderDetail>()
           {
                new(),
                new(),
                new(),
                new(),
                new(),
            };

        return sampleOrderDetails;
    }
}
