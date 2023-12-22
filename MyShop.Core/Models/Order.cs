namespace MyShop.Core.Models;

public class Order
{
    public string Id
    {
        get; set;
    }

    public string UserId
    {
        get; set;
    }

    public DateTime OrderDate
    {

        get; set;
    }

    public string Status
    {
        get; set;
    }

    public string Description
    {
        get; set;
    }

    public decimal TotalPrice
    {
        get; set;
    }

    public decimal FinalPrice
    {
        get; set;
    }

    public ICollection<OrderDetail> Details
    {
        get; set;
    }
}
