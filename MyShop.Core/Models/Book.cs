namespace MyShop.Core.Models;

public class Book
{
    public int Id
    {
        get; set;
    }

    public string Name
    {
        get; set;
    }


    public string Image
    {
        get; set;
    }

    public decimal PurchasePrice
    {
        get; set;
    }

    public decimal SellingPrice
    {
        get; set;
    }

    public string Author
    {

        get; set;
    }

    public int PublishedYear
    {
        get; set;
    }

    public decimal RatingsAverage
    {
        get; set;
    }

    public int Quantity
    {
        get; set;
    }

    public string Description
    {
        get; set;
    }

    public int CategoryId
    {
        get; set;
    }
}
