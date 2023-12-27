
using System.Text.Json;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Helpers;
using MyShop.Core.Http;
using MyShop.Core.Models;

namespace MyShop.Core.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public OrderRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(Order, string, int)> CreateAOrderAsync(AddOrder addOrder)
    {
        var order = new Order();
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {

            var client = _httpClientFactory.CreateClient("Backend");
            var content = new StringContent(JsonSerializer.Serialize(addOrder), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"orders", content);

            if (response.IsSuccessStatusCode)
            {

                var responseContent = response.Content.ReadAsStringAsync().Result;
                var orders = JsonSerializer.Deserialize<HttpDataSchemaResponse<Order>>(responseContent);
                order = orders.Data;
            }
            else
            {

                message = response.ReasonPhrase;
                ERROR_CODE = (int)response.StatusCode;
            }
        }
        catch (Exception ex)
        {

            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (order, message, ERROR_CODE);
    }

    public async Task<(IEnumerable<Order>, int, string, int)> GetAllOrdersAsync()
    {
        var Orders = new List<Order>();
        var totalOrders = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;
        var paramBuilder = new HttpSearchParamsBuilder();

        paramBuilder.Append("page", 1);
        paramBuilder.Append("limit", 2);

        try
        {
            var client = _httpClientFactory.CreateClient("Backend");
            var response = await client.GetAsync($"orders");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var OrderResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<IEnumerable<Order>>>(content);
                Orders = OrderResponse.Data.ToList();
                totalOrders = int.Parse(response.Headers.GetValues("x-total-count").FirstOrDefault());
            }
            else
            {
                message = response.ReasonPhrase;
                ERROR_CODE = (int)response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (Orders, totalOrders, message, ERROR_CODE);
    }

    public async Task<(IEnumerable<Order>, int, string, int)> GetAllOrdersAsync(string searchParams)
    {
        var Orders = new List<Order>();
        var totalItems = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            var client = _httpClientFactory.CreateClient("Backend");
            var response = await client.GetAsync($"Orders?{searchParams}");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var OrderResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<IEnumerable<Order>>>(content);
                Orders = OrderResponse.Data.ToList();

                totalItems = int.Parse(response.Headers.GetValues("x-total-count").FirstOrDefault());
            }
            else
            {
                message = response.ReasonPhrase;
                ERROR_CODE = (int)response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (Orders, totalItems, message, ERROR_CODE);
    }
}
