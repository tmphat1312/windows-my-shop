using System.Text;
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

    public async Task<(Category, string, int)> CreateCategoryAsync(Category category)
    {
        var returnedCategory = new Category();
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var httpContent = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("categories", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<Category>>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                returnedCategory = httpResponse.Data;
                message = "Category created successfully.";
            }
            else
            {
                ERROR_CODE = (int)response.StatusCode;

                if (ERROR_CODE == 400)
                {
                    message = httpResponse.Error?.Message;
                }
                else if (ERROR_CODE == 500)
                {
                    message = httpResponse.Message;
                }
                else
                {
                    message = "Something went wrong. Please try again later.";
                }
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (returnedCategory, message, ERROR_CODE);
    }

    public async Task<(string, int)> DeleteCategoryAsync(Category category)
    {
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var response = await client.DeleteAsync($"categories/{category.Id}");

            if (response.IsSuccessStatusCode)
            {
                message = "Category deleted successfully.";
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<Book>>(content);
                message = httpResponse.Error.Message;
                ERROR_CODE = (int)response.StatusCode;
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (message, ERROR_CODE);
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

    public async Task<(Category, string, int)> UpdateCategoryAsync(Category category)
    {
        var returnedCategory = new Category();
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            var updateCategory = new Category()
            {
                Name = category.Name,
                Description = category.Description,
            };

            using var client = _httpClientFactory.CreateClient("Backend");
            using var httpContent = new StringContent(JsonSerializer.Serialize(updateCategory), Encoding.UTF8, "application/json");

            var response = await client.PatchAsync($"categories/{category.Id}", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<Category>>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                returnedCategory = httpResponse.Data;
                returnedCategory.Id = category.Id;
                message = "Category updated successfully.";
            }
            else
            {
                ERROR_CODE = (int)response.StatusCode;

                if (ERROR_CODE == 400)
                {
                    message = httpResponse.Error?.Message;
                }
                else if (ERROR_CODE == 500)
                {
                    message = httpResponse.Message;
                }
                else
                {
                    message = "Something went wrong. Please try again later.";
                }
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            ERROR_CODE = -1;
        }

        return (returnedCategory, message, ERROR_CODE);
    }
}
