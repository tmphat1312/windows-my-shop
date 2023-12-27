using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Services;
public interface IBookDataService
{
    public string SearchParams
    {
        get; set;
    }
    public bool IsDirty
    {
        get; set;
    }

    public Task<(IEnumerable<Book>, int, string, int)> LoadDataAsync();
    public (IEnumerable<Book>, int, string, int) GetData();

    public Task<(Book, string, int)> CreateBookAsync(Book book);

    public Task<(string, int)> DeleteBookAsync(Book book);

    public Task<(Book, string, int)> UpdateBookAsync(Book book);

}
