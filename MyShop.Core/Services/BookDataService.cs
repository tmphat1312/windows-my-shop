using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class BookDataService : IBookDataService
{
    private readonly IBookRepository _bookRepository;

    // (books, totalItems, errorMessage, ErrorCode)
    private (IEnumerable<Book>, int, string, int) _bookDataTuple;

    public bool IsInitialized => _bookDataTuple.Item1 is not null;
    public bool IsDirty { get; set; } = true;

    private string _searchParams;
    public string SearchParams
    {

        get => _searchParams;
        set
        {
            _searchParams = value;
            IsDirty = true;
        }
    }

    public (IEnumerable<Book>, int, string, int) GetData() => _bookDataTuple;

    public BookDataService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<(IEnumerable<Book>, int, string, int)> LoadDataAsync()
    {
        _bookDataTuple = await _bookRepository.GetAllBooksAsync(SearchParams);

        return _bookDataTuple;
    }

    public async Task<(Book, string, int)> CreateBookAsync(Book book)
    {
        return await Task.Run(async () => await _bookRepository.CreateABookAsync(book));
    }

    public async Task<(string, int)> DeleteBookAsync(Book book)
    {
        return await Task.Run(async () => await _bookRepository.DeleteBookAsync(book));
    }

    public async Task<(Book, string, int)> UpdateBookAsync(Book book)
    {
        return await Task.Run(async () => await _bookRepository.UpdateBookAsync(book));
    }
}
