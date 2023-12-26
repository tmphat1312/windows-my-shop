using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;
public interface IOrderRepository
{
    Task<(Order, string, int)> CreateAOrderAsync(AddOrder addOrder);
    Task<(IEnumerable<Order>, int, string, int)> GetAllOrdersAsync();

  
    Task<(IEnumerable<Order>, int, string, int)> GetAllOrdersAsync(string searchParams);
}
