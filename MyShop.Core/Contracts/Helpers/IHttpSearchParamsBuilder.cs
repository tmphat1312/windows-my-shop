namespace MyShop.Core.Contracts.Helpers;
public interface IHttpSearchParamsBuilder
{
    public void Append(string key, object value);
    public string GetQueryString();
}
