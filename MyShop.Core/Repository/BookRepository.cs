using System.Text.Json;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Http;
using MyShop.Core.Models;

namespace MyShop.Core.Repository;
public class BookRepository : IBookRepository
{
    private const string BaseUrl = "http://localhost:8080/api/v1";

    public BookRepository()
    {
    }

    public async Task<(IEnumerable<Book>, int, string, int)> GetAllBooksAsync()
    {
        var books = new List<Book>();
        var totalBooks = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"{BaseUrl}/books");

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
