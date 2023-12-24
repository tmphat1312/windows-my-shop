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
using Newtonsoft.Json.Linq;

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
                var jsonObject = JObject.Parse(jsonString);
                result = jsonObject["data"].ToObject<List<User>>();
            }
            return result;
        }
        catch (Exception ex)
        {
            // Log lỗi hoặc xử lý ngoại lệ ở đây
            Console.WriteLine(ex.Message);
            return result;
        }
    }

    public async Task<string> CreateUserAsync(User user)
    {
        string result = "";
        var apiUrl = "http://localhost:8080/api/v1/users";
        try
        {
            //using (var content = new MultipartFormDataContent())
            //{
            //    content.Add(new StringContent(user.Name), "name");
            //    content.Add(new StringContent(user.Image), "email");
            //    content.Add(new StringContent(user.Password), "password");
            //    content.Add(new StringContent(user.Role), "role");

            //    var selectedImageFile = null
            //    if (selectedImageFile != null)
            //    {
            //        var streamContent = new StreamContent(await selectedImageFile.OpenStreamForReadAsync());
            //        content.Add(streamContent, "image", selectedImageFile.Name);
            //    }

            //    HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        return "User created successfully";
            //    }
            //    else
            //    {
            //        return "Error: " + await response.Content.ReadAsStringAsync();
            //    }
            //}
            return result;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            return result;
        }
    }

}
