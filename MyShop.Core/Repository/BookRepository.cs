using System.Text.Json;
using MyShop.Core.Contracts.Repository;
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

    public async Task<(Book, string, int)> CreateABookAsync(Book newBook)
    {
        var returnedBook = new Book();
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var content = new MultipartFormDataContent
            {
                { new StringContent(newBook.Name), "name" },
                { new StringContent(newBook.Description), "description" },
                { new StringContent(newBook.Author), "author" },
                { new StringContent(newBook.CategoryId), "category" },
                { new StringContent(newBook.SellingPrice.ToString()), "sellingPrice" },
                { new StringContent(newBook.PurchasePrice.ToString()), "purchasePrice" },
                { new StringContent(newBook.Quantity.ToString()), "quantity" },
                { new StringContent(newBook.PublishedYear.ToString()), "publishedYear" }
            };

            if (newBook.ImageBytes != null)
            {
                var memoryStream = new MemoryStream(newBook.ImageBytes);
                var streamContent = new StreamContent(memoryStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(streamContent, "image", "filename.jpg");
            }

            var response = await client.PostAsync("books", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<Book>>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                returnedBook = httpResponse.Data;
                message = "Book created successfully.";
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

        return (returnedBook, message, ERROR_CODE);
    }

    public async Task<(IEnumerable<Book>, int, string, int)> GetAllBooksAsync(string searchParams)
    {
        var books = new List<Book>();
        var totalItems = 0;
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var response = await client.GetAsync($"books?{searchParams}");
            var content = response.Content.ReadAsStringAsync().Result;
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<IEnumerable<Book>>>(content);

            if (response.IsSuccessStatusCode)
            {
                books = httpResponse.Data.ToList();
                totalItems = int.Parse(response.Headers.GetValues("x-total-count").FirstOrDefault());
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

        return (books, totalItems, message, ERROR_CODE);
    }
}
