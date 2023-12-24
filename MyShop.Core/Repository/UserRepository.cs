using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
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
    private string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY1ODJjMDI2MzdkNzRmMDMyMDkwNTU1NCIsImlhdCI6MTcwMzQ1MTE0NywiZXhwIjoxNzAzNDUyOTQ3fQ.tK3ucqI9hp5jNyWyhQVDaKm0WI6oA7TJBYsbwb88MLA";
    public UserRepository(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }

   
    public async Task<(bool isSuccess, string Message, int ErrorCode)> CreateUserAsync(User user)
    {
        bool isSuccess = false;
        string message = "";
        int errorCode = 0;
        try
        {
            var apiUrl = "http://localhost:8080/api/v1/users";
            using (var content = new MultipartFormDataContent())
            {
                content.Add(new StringContent(user.Name), "name");
                content.Add(new StringContent(user.Email), "email");
                content.Add(new StringContent(user.Password), "password");
                content.Add(new StringContent(user.Role), "role");

                if (user.ImageBytes != null)
                {
                    var memoryStream = new MemoryStream(user.ImageBytes);
                    var streamContent = new StreamContent(memoryStream);
                    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                    content.Add(streamContent, "image", "filename.jpg"); // Thay "filename.jpg" bằng tên file thích hợp
                }

                HttpResponseMessage response = await _httpClient.PostAsync(apiUrl, content);
                string responseBody = await response.Content.ReadAsStringAsync();

                var jsonResponse = JObject.Parse(responseBody);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Created: // 200
                        isSuccess = true;
                        message = "Create user successfully.";
                        errorCode = 201;
                        break;

                    case System.Net.HttpStatusCode.Unauthorized: // 401
                        isSuccess = false;
                        message = jsonResponse["message"]?.ToString() ?? "Unauthorized access.";
                        errorCode = 401;
                        break;

                    case System.Net.HttpStatusCode.BadRequest: // 400
                        isSuccess = false;
                        message = jsonResponse["error"]?["errorMessage"]?.ToString();
                        errorCode = 401;
                        break;

                    default:
                        throw new Exception("System Error. Please contact administrator.");
                }
            }
        }
        catch (Exception ex)
        {
            message = ex.Message;
            errorCode = 500;
            isSuccess = false;
        }

        return (isSuccess, message, errorCode);
    }

    public async Task<(bool isSuccess, List<User> Data, string Message, int ErrorCode)> GetAllUsersAsync()
    {
        bool isSuccess = false;
        List<User> data = new List<User>();
        string message = "";
        int errorCode = 0;
        try
        {
            var apiUrl = "http://localhost:8080/api/v1/users";
            var response = await _httpClient.GetAsync(apiUrl);
            var jsonString = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var jsonObject = JObject.Parse(jsonString);
                var users = jsonObject["data"]?.ToObject<List<User>>() ?? new List<User>();
                isSuccess = true;
                data = users;
                message = "Success";
                errorCode = 200;
            }
            else
            {
                message = "Error occurred";
                errorCode = (int)response.StatusCode;
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) // 401
                {
                    var errorObject = JObject.Parse(jsonString);
                    message = errorObject["message"]?.ToString() ?? "Unauthorized access.";
                }

               
            }
        }
        catch (Exception ex)
        {
            isSuccess = false;
            message = ex.Message;
            errorCode = 500;
        }

        return (isSuccess, data, message, errorCode);
    }


}
