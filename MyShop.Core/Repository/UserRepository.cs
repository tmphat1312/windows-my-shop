using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Models;
using Newtonsoft.Json;

namespace MyShop.Core.Repository;
public class UserRepository : IUserRepository
{
    private readonly HttpClient _httpClient;
    private string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1ODJjMDI2MzdkNzRmMDMyMDkwNTU1NCIsImlhdCI6MTcwMzQxNTQyNCwiZXhwIjoxNzAzNDE3MjI0fQ.h5cY7TPv9WjU_f6aJKnYzfvAnzjjKoYDbEKAUvX731k";
    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

    public Task<string> CreateUserAsync(User user) => throw new NotImplementedException();

    public async Task<List<User>> GetAllUsersAsync()
    {
        List<User> result = new List<User>();

        try
        {
            var apiUrl = "http://localhost:8080/api/v1/users";
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return (List<User>)JsonConvert.DeserializeObject<IEnumerable<User>>(jsonString);
            }
            else
            {
                return new List<User>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return result;
        }
    }

}
