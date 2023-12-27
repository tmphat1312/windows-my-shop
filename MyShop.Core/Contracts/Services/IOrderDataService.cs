using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IOrderDataService
{
    public string SearchParams
    {
        get; set;
    }
    public bool IsDirty
    {
        get; set;
    }
    public Task<(IEnumerable<Order>, int, string, int)> LoadDataAsync();
    public (IEnumerable<Order>, int, string, int) GetData();

    Task<(Order, string, int)> CreateAOrderAsync(List<AddOrderDetail> addOrderDetail);

    Task<(Order, string, int)> UpdateOrderAsync(Order order);

    Task<(string, int)> DeleteOrderAsync(Order order);
}
