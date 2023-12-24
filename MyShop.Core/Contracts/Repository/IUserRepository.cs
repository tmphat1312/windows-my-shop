using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;
public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();

    Task<string> CreateUserAsync(User user);
}
