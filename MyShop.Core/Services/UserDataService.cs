﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class UserDataService : IUserDataService
{
    private readonly IUserRepository _userRepository;
    private List<User> _allUsers;

    public UserDataService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        _allUsers = new List<User>();
    }

    private static IEnumerable<User> AllUsers()
    {
        // The following is order summary data
        var allUsers = new List<User>();

        for (var i = 0; i < 10; i++)
        {
            allUsers.Add(new User
            {
                ID = $"{i}",
                Name = $"User{i}",
                Email = $"user{i}@example.com",
                Role = "Member",
                Image = null,
                CreatedAt = DateTime.Now,
            });
        }

        return allUsers;
    }

    public async Task<IEnumerable<User>> GetListUserDetailsDataAsync()
    {
        _allUsers =  await _userRepository.GetAllUsersAsync();

        await Task.CompletedTask;
        return _allUsers;
       
    }
}
