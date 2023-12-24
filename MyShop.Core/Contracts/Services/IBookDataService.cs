using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IBookDataService
{
    public Task<(IEnumerable<Book>, int, string, int)> GetContentGridDataAsync();
    public Task<(IEnumerable<Book>, int, string, int)> LoadBookAsync();
    public (IEnumerable<Book>, int, string, int) GetData();

}
