using System.Net.Http.Headers;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Models;
using Newtonsoft.Json;

namespace MyShop.Core.Repository;
public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;
    private string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1ODJjMDI2MzdkNzRmMDMyMDkwNTU1NCIsImlhdCI6MTcwMzM0MDY4MSwiZXhwIjoxNzAzMzQyNDgxfQ.0iwXc6kXE836SzkahPQkzuw7KyTmaNuifrRYK3h4T3M";

    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        try
        {
            var apiUrl = "http://localhost:8080/api/v1/users";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<User>>(jsonString);
            }
            else
            {
                return new List<User>();
            }
        }
        catch (Exception ex)
        {
            // Log lỗi hoặc xử lý ngoại lệ ở đây
            Console.WriteLine(ex.Message);
            return new List<User>();
        }
    }
}
