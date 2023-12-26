using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;

public interface ICategoryDataService
{
    public List<Category> Categories
    {
        get;
    }

    public Task<(IEnumerable<Category>, string, int)> LoadDataAsync();
    public (IEnumerable<Category>, string, int) GetData();
}
