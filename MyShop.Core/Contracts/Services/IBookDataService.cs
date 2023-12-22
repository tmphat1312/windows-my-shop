using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IBookDataService
{
    Task<IEnumerable<Book>> GetContentGridDataAsync();
}
