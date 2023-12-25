namespace MyShop.Core.Http;

public class HttpSortObject
{
    public string Name
    {
        get; set;
    }

    public string Value
    {
        get; set;
    }

    public bool IsAscending
    {
        get; set;
    }

    public string BuildSortString()
    {
        var prefix = IsAscending ? "" : "-";
        var sortString = $"{prefix}{Value}";

        return sortString;
    }

    public string SortString => BuildSortString();
}
