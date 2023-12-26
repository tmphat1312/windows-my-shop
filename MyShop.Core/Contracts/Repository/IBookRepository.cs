using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;

public interface IBookRepository
{
    /// <summary>
    /// Get all books asynchronously.
    /// </summary>
    /// <returns>
    /// (IEnumerable<Book>, int, string) tuple.
    /// IEnumerable<Book> is the collection of books.
    /// int is the total number of books.
    /// string is the message.
    /// int is the error code. 0 means no error. -1 means exception.
    /// </returns>
    Task<(IEnumerable<Book>, int, string, int)> GetAllBooksAsync(string searchParams);

    /// <summary>
    /// Create a book asynchronously.
    /// </summary>
    /// <param name="newBook"></param>
    /// <returns>
    /// Book is the newly created book.
    /// string is the message.
    /// int is the error code. 0 means no error. -1 means exception.
    /// </returns>
    Task<(Book, string, int)> CreateABookAsync(Book newBook);

    /// <summary>
    /// Delete a book asynchronously.
    /// </summary>
    /// <param name="book"></param>
    /// <returns>
    /// string: message.
    /// int: error code. 0 means no error. -1 means exception.
    /// </returns>
    Task<(string, int)> DeleteBookAsync(Book book);

    Task<(Book, string, int)> UpdateBookAsync(Book book);
}
