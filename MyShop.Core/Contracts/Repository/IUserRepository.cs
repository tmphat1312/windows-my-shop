using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;
public interface IUserRepository
{
    Task<(bool isSuccess, List<User> Data, string Message, int ErrorCode)> GetAllUsersAsync();

    Task<(bool isSuccess, string Message, int ErrorCode)> CreateUserAsync(User user);
}
