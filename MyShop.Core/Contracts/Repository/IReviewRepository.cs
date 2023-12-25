using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;
public interface IReviewRepository
{
    /// <summary>
    /// Get reviews of a book asynchronously.
    /// </summary>
    /// <returns>
    /// (IEnumerable<Review>, int, string) tuple.
    /// IEnumerable<Review> is the collection of review of the given book.
    /// int is the total number of reviews.
    /// string is the message.
    /// int is the error code. 0 means no error. -1 means exception.
    /// </returns>
    Task<(IEnumerable<Review>, int, string, int)> GetAllReviewsAsync(string bookId);
}
