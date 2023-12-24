using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;
public class BookDataService : IBookDataService
{
    private List<Book> _books;
    private (IEnumerable<Book>, int, string, int) _bookDataTuple;
    private readonly IBookRepository _bookRepository;

    public BookDataService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<(IEnumerable<Book>, int, string, int)> GetContentGridDataAsync()
    {
        if (_books is null)
        {
            _bookDataTuple = await _bookRepository.GetAllBooksAsync();
            _books = new List<Book>(_bookDataTuple.Item1);
        }

        return _bookDataTuple;
    }

    public async Task<(IEnumerable<Book>, int, string, int)> LoadBookAsync()
    {
        if (_books is null)
        {
            _bookDataTuple = await _bookRepository.GetAllBooksAsync();
            _books = new List<Book>(_bookDataTuple.Item1);
        }

        return _bookDataTuple;
    }

    public (IEnumerable<Book>, int, string, int) GetData() => _bookDataTuple;
}
