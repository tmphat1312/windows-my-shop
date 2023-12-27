using MyShop.Core.Models;

namespace MyShop.Core.Contracts.Repository;

public interface ICategoryRepository
{
    public Task<(IEnumerable<Category>, string, int)> GetCategoriesAsync();

    public Task<(Category, string, int)> CreateCategoryAsync(Category category);

    public Task<(Category, string, int)> UpdateCategoryAsync(Category category);

    Task<(string, int)> DeleteCategoryAsync(Category category);

    public Task<(string, int)> ImportDataAsync(IEnumerable<Category> categories);
}
