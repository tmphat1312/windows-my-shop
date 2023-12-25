using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class ReviewDataService : IReviewDataService
{
    private readonly IReviewRepository _reviewRepository;
    private (IEnumerable<Review>, int, string, int) _reviewDataTuple;

    public bool IsInitialized => _reviewDataTuple.Item1 is not null;

    public string BookId
    {
        get; set;
    }

    public ReviewDataService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public (IEnumerable<Review>, int, string, int) GetData() => _reviewDataTuple;
    public async Task<(IEnumerable<Review>, int, string, int)> LoadDataAsync()
    {
        _reviewDataTuple = await _reviewRepository.GetAllReviewsAsync(BookId);

        return _reviewDataTuple;
    }
}
