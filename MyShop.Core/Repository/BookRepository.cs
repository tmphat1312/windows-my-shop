using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
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
                { new StringContent(newBook.Name ?? ""), "name" },
                { new StringContent(newBook.Description ?? ""), "description" },
                { new StringContent(newBook.Author ?? ""), "author" },
                { new StringContent(newBook.CategoryId ?? ""), "category" },
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

    public async Task<(string, int)> DeleteBookAsync(Book book)
    {
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var response = await client.DeleteAsync($"books/{book.Id}");

            if (response.IsSuccessStatusCode)
            {
                message = "Book deleted successfully.";
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

    public async Task<(Book, string, int)> UpdateBookAsync(Book book)
    {
        var returnedBook = new Book();
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            using var content = new MultipartFormDataContent
            {
                { new StringContent(book.Name ?? ""), "name" },
                { new StringContent(book.Description ?? ""), "description" },
                { new StringContent(book.Author ?? ""), "author" },
                //{ new StringContent(book.CategoryId ?? ""), "category" },
                { new StringContent(book.SellingPrice.ToString()), "sellingPrice" },
                { new StringContent(book.PurchasePrice.ToString()), "purchasePrice" },
                { new StringContent(book.Quantity.ToString()), "quantity" },
                { new StringContent(book.PublishedYear.ToString()), "publishedYear" }
            };

            if (book.ImageBytes != null)
            {
                var memoryStream = new MemoryStream(book.ImageBytes);
                var streamContent = new StreamContent(memoryStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                content.Add(streamContent, "image", "filename.jpg");
            }

            var response = await client.PatchAsync($"books/{book.Id}", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<Book>>(responseContent);

            if (response.IsSuccessStatusCode)
            {
                returnedBook = httpResponse.Data;
                message = "Book updated successfully.";
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
                else if (ERROR_CODE == 401)
                {
                    message = "Your login session is expired.";
                }
                else
                {
                    message = "Something broke!!!";
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

    public async Task<(string, int)> ImportDataAsync(IEnumerable<Book> books)
    {
        var message = string.Empty;
        var ERROR_CODE = 0;

        try
        {
            using var client = _httpClientFactory.CreateClient("Backend");
            var options = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            using var httpContent = new StringContent(JsonSerializer.Serialize(books, options), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("books", httpContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                message = "Books imported successfully.";
            }
            else
            {
                var httpResponse = JsonSerializer.Deserialize<HttpDataSchemaResponse<Book>>(responseContent);
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

        return (message, ERROR_CODE);
    }
}
