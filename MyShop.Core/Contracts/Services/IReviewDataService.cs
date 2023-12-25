using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IReviewDataService
{
    public string BookId
    {
        set; get;
    }
    public Task<(IEnumerable<Review>, int, string, int)> LoadDataAsync();
    public (IEnumerable<Review>, int, string, int) GetData();
}
