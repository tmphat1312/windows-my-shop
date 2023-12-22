using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class UserDataService : IUserDataService
{
    private List<User> _allUsers;

    public UserDataService()
    {
    }

    private static IEnumerable<User> AllUsers()
    {
        // The following is order summary data
        var allUsers = new List<User>();

        for (var i = 0; i < 10; i++)
        {
            allUsers.Add(new User
            {
                ID = i,
                Name = $"User{i}",
                Email = $"user{i}@example.com",
                Password = $"Password{i}!",
                Role = "Member",
                Image = "Assets/user.png",
                CreateAt = DateTime.Now,
                PasswordUpdateAt = DateTime.Now.AddDays(-30) // Giả sử mật khẩu được cập nhật 30 ngày trước
            });
        }

        return allUsers;
    }

    public async Task<IEnumerable<User>> GetListUserDetailsDataAsync()
    {
        _allUsers ??= new List<User>(AllUsers());

        await Task.CompletedTask;
        return _allUsers;
    }
}
