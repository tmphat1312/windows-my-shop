using System.Text.Json;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Http;
using MyShop.Core.Models;

namespace MyShop.Core.Repository;
public class CategoryRepository : ICategoryRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public CategoryRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(IEnumerable<Category>, string, int)> GetCategoriesAsync()
    {
        var categories = new List<Category>();
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var response = await client.GetAsync($"categories?limit=1000");
            var content = response.Content.ReadAsStringAsync().Result;
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<IEnumerable<Category>>>(content);

            if (response.IsSuccessStatusCode)
            {
                categories = httpResponse.Data.ToList();
            }
            else
            {
                message = httpResponse.Error.Message;
                ERROR_CODE = (int)response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (categories, message, ERROR_CODE);
    }
}
