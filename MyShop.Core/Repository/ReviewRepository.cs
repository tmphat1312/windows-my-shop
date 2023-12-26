using System.Text.Json;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Http;
using MyShop.Core.Models;

namespace MyShop.Core.Repository;
public class ReviewRepository : IReviewRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ReviewRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(IEnumerable<Review>, int, string, int)> GetAllReviewsAsync(string bookId)
    {
        var reviews = new List<Review>();
        var totalItems = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            var client = _httpClientFactory.CreateClient("Backend");
            var response = await client.GetAsync($"reviews?book={bookId}");
            var content = response.Content.ReadAsStringAsync().Result;
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<IEnumerable<Review>>>(content);

            if (response.IsSuccessStatusCode)
            {
                reviews = httpResponse.Data.ToList();
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

        return (reviews, totalItems, message, ERROR_CODE);
    }
}
