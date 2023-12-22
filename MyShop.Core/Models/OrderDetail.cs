namespace MyShop.Core.Models;

public class OrderDetail
{
    public string OrderId
    {
        get; set;
    }

    public string BookId
    {
        get; set;
    }

    public int Quantity
    {
        get; set;
    }

    public decimal Price
    {
        get; set;
    }
}
