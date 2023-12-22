using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class BookDataService : IBookDataService
{
    private List<Book> _books;

    private static IEnumerable<Book> Books()
    {
        var sampleBooks = new List<Book>()
        {
           new() {
                Id = 1,
                Name = "Book 1",
                Image = "book1.jpg",
                PurchasePrice = 15.99m,
                SellingPrice = 24.99m,
                Author = "Author 1",
                PublishedYear = 2020,
                RatingsAverage = 4.5m,
                Quantity = 50,
                Description = "Description of Book 1",
                CategoryId = 1
            },
            new() {
                Id = 2,
                Name = "Book 2",
                Image = "book2.jpg",
                PurchasePrice = 12.49m,
                SellingPrice = 19.99m,
                Author = "Author 2",
                PublishedYear = 2018,
                RatingsAverage = 4.0m,
                Quantity = 35,
                Description = "Description of Book 2",
                CategoryId = 2
            },
            new() {
                Id = 3,
                Name = "Book 3",
                Image = "book3.jpg",
                PurchasePrice = 9.99m,
                SellingPrice = 14.99m,
                Author = "Author 3",
                PublishedYear = 2019,
                RatingsAverage = 3.8m,
                Quantity = 42,
                Description = "Description of Book 3",
                CategoryId = 3
            },
            new() {
                Id = 4,
                Name = "Book 4",
                Image = "book4.jpg",
                PurchasePrice = 18.75m,
                SellingPrice = 27.99m,
                Author = "Author 4",
                PublishedYear = 2017,
                RatingsAverage = 4.2m,
                Quantity = 28,
                Description = "Description of Book 4",
                CategoryId = 1
            },
        };

        return sampleBooks;
    }

    public async Task<IEnumerable<Book>> GetContentGridDataAsync()
    {
        _books ??= new List<Book>(Books());

        await Task.CompletedTask;

        return _books;
    }
}
