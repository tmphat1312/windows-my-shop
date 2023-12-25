using System.Text;
using MyShop.Core.Contracts.Helpers;

namespace MyShop.Core.Helpers;

public class HttpSearchParamsBuilder : IHttpSearchParamsBuilder
{
    public Dictionary<string, string> Params
    {
        get;
    } = new Dictionary<string, string>();

    public HttpSearchParamsBuilder()
    {
    }

    public void Append(string key, object value)
    {
        if (value is null)
        {
            return;
        }

        Params.Add(key, value.ToString());
    }

    public string GetQueryString()
    {
        var sb = new StringBuilder();

        //sb.Append("?");

        foreach (var (key, value) in Params)
        {
            sb.Append($"{key}={value}&");
        }

        return sb.ToString().TrimEnd('&');
    }
}
