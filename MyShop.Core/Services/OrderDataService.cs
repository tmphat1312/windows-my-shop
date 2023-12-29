using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class OrderDataService : IOrderDataService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IAuthenticationService _authenticationService;

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

    public OrderDataService(IOrderRepository orderRepository, IAuthenticationService authenticationService)
    {
        _orderRepository = orderRepository;
        _authenticationService = authenticationService;
    }

    public async Task<(IEnumerable<Order>, int, string, int)> LoadDataAsync()
    {
        _orderDataTuple = await _orderRepository.GetAllOrdersAsync(SearchParams);

        return _orderDataTuple;
    }

    public async Task<(Order, string, int)> CreateAOrderAsync(List<AddOrderDetail> addOrderDetail)
    {
        var userId = _authenticationService.GetUserId();
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
