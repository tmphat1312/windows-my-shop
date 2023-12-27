using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class OrderDataService : IOrderDataService
{
    private readonly IOrderRepository _orderRepository;

    // (orders, totalItems, errorMessage, ErrorCode)
    private (IEnumerable<Order>, int, string, int) _orderDataTuple;

    public bool IsInitialized => _orderDataTuple.Item1 is not null;
    public bool IsDirty { get; set; } = true;

    private string _searchParams;
    public string SearchParams
    {

        get => _searchParams;
        set
        {
            _searchParams = value;
            IsDirty = true;
        }
    }

    public (IEnumerable<Order>, int, string, int) GetData() => _orderDataTuple;

    public OrderDataService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<(IEnumerable<Order>, int, string, int)> LoadDataAsync()
    {
        _orderDataTuple = await _orderRepository.GetAllOrdersAsync();

        return _orderDataTuple;
    }

    public async Task<(Order, string, int)> CreateAOrderAsync(List<AddOrderDetail> addOrderDetail)
    {
        string userId = "65889e531ceae159d7937a4e";
        var addOrder = new AddOrder
        {
            UserId = userId,
            OrderDetails = addOrderDetail
        };

        return await _orderRepository.CreateAOrderAsync(addOrder);
    }

    public async Task<(Order, string, int)> UpdateOrderAsync(Order order)
    {

        return await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task<(string, int)> DeleteOrderAsync(Order order)
    {
        return await _orderRepository.DeleteOrderAsync(order);
    }
}
