namespace MyShop.Core.Http;

public class HttpFilterObject
{
    public string Key
    {
        get; set;
    }

    public string Value
    {
        get; set;
    }


    public string FilterString => Value;
}
