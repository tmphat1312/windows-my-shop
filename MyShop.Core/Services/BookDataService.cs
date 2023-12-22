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
                PurchasePrice = 15.99,
                SellingPrice = 24.99,
                Author = "Author 1",
                PublishedYear = 2020,
                RatingsAverage = 4.5,
                Quantity = 50,
                Description = "Description of Book 1",
                CategoryId = 1
            },
            new() {
                Id = 2,
                Name = "Book 2",
                Image = "book2.jpg",
                PurchasePrice = 12.49,
                SellingPrice = 19.99,
                Author = "Author 2",
                PublishedYear = 2018,
                RatingsAverage = 4.0,
                Quantity = 35,
                Description = "Description of Book 2",
                CategoryId = 2
            },
            new() {
                Id = 3,
                Name = "Book 3",
                Image = "book3.jpg",
                PurchasePrice = 9.99,
                SellingPrice = 14.99,
                Author = "Author 3",
                PublishedYear = 2019,
                RatingsAverage = 3.8,
                Quantity = 42,
                Description = "Description of Book 3",
                CategoryId = 3
            },
            new() {
                Id = 4,
                Name = "Book 4",
                Image = "book4.jpg",
                PurchasePrice = 18.75,
                SellingPrice = 27.99,
                Author = "Author 4",
                PublishedYear = 2017,
                RatingsAverage = 4.2,
                Quantity = 28,
                Description = "Description of Book 4",
                CategoryId = 1
            },
            new() {
                Id = 5,
                Name = "Book 5",
                Image = "book5.jpg",
                PurchasePrice = 18.75,
                SellingPrice = 27.99,
                Author = "Author 5",
                PublishedYear = 2017,
                RatingsAverage = 5.2,
                Quantity = 28,
                Description = "Description of Book 5",
                CategoryId = 1
            },
            new() {
                Id = 6,
                Name = "Book 6",
                Image = "book6.jpg",
                PurchasePrice = 18.75,
                SellingPrice = 27.99,
                Author = "Author 6",
                PublishedYear = 2017,
                RatingsAverage = 6.2,
                Quantity = 28,
                Description = "Description of Book 6",
                CategoryId = 1
            },
            new() {
                Id = 7,
                Name = "Book 7",
                Image = "book7.jpg",
                PurchasePrice = 18.75,
                SellingPrice = 27.99,
                Author = "Author 7",
                PublishedYear = 2017,
                RatingsAverage = 7.2,
                Quantity = 28,
                Description = "Description of Book 7",
                CategoryId = 1
            },
            new() {
                Id = 8,
                Name = "Book 8",
                Image = "book8.jpg",
                PurchasePrice = 18.75,
                SellingPrice = 27.99,
                Author = "Author 8",
                PublishedYear = 2017,
                RatingsAverage = 8.2,
                Quantity = 28,
                Description = "Description of Book 8",
                CategoryId = 1
            },
            new() {
                Id = 9,
                Name = "Book 9",
                Image = "book9.jpg",
                PurchasePrice = 18.75,
                SellingPrice = 27.99,
                Author = "Author 9",
                PublishedYear = 2017,
                RatingsAverage = 9.2,
                Quantity = 28,
                Description = "Description of Book 9",
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
