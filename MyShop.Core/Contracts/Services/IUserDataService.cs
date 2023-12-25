using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IUserDataService
{
    Task<(bool isSuccess, IEnumerable<User> Data, string Message, int ErrorCode)> GetListUserDetailsDataAsync();
    Task<(bool isSuccess, string Message, int ErrorCode)> CreateUserAsync(User user);


}
