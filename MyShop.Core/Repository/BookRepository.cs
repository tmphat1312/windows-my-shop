using System.Text.Json;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Helpers;
using MyShop.Core.Http;
using MyShop.Core.Models;

namespace MyShop.Core.Repository;

public class BookRepository : IBookRepository
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BookRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(IEnumerable<Book>, int, string, int)> GetAllBooksAsync()
    {
        var books = new List<Book>();
        var totalBooks = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;
        var paramBuilder = new HttpSearchParamsBuilder();

        paramBuilder.Append("page", 1);
        paramBuilder.Append("limit", 2);

        try
        {
            var client = _httpClientFactory.CreateClient("Backend");
            var response = await client.GetAsync($"books?{paramBuilder.GetQueryString()}");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var bookResponse = JsonSerializer.Deserialize<HttpArrayDataSchemaResponse<Book>>(content);
                books = bookResponse.Data.ToList();
                totalBooks = int.Parse(response.Headers.GetValues("x-total-count").FirstOrDefault());
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

        return (books, totalBooks, message, ERROR_CODE);
    }

    public async Task<(IEnumerable<Book>, int, string, int)> GetAllBooksAsync(string searchParams)
    {
        var books = new List<Book>();
        var totalBooks = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            var client = _httpClientFactory.CreateClient("Backend");
            var response = await client.GetAsync($"books?{searchParams}");

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var bookResponse = JsonSerializer.Deserialize<HttpArrayDataSchemaResponse<Book>>(content);
                books = bookResponse.Data.ToList();
                totalBooks = int.Parse(response.Headers.GetValues("x-total-count").FirstOrDefault());
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

        return (books, totalBooks, message, ERROR_CODE);
    }
}
