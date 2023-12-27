using MyShop.Core.Contracts.Repository;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.Core.Services;

public class CategoryDataService : ICategoryDataService
{
    private readonly ICategoryRepository _categoryRepository;
    private (List<Category>, string, int) _categoryTuple;

    public List<Category> Categories => _categoryTuple.Item1;
    private bool IsAlreadyFetched = false;
    public bool IsDirty = false;

    public CategoryDataService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public (IEnumerable<Category>, string, int) GetData() => _categoryTuple;

    public async Task<(IEnumerable<Category>, string, int)> LoadDataAsync()
    {
        if (!IsAlreadyFetched || IsDirty)
        {
            _categoryTuple = ((List<Category>, string, int))await _categoryRepository.GetCategoriesAsync();
            IsAlreadyFetched = true;
        }

        return _categoryTuple;
    }

    public Task<(Category, string, int)> AddCategoryAsync(Category category)
    {
        IsDirty = true;
        return Task.Run(async () => await _categoryRepository.CreateCategoryAsync(category));
    }

    public Task<(Category, string, int)> UpdateCategoryAsync(Category category)
    {
        IsDirty = true;
        return Task.Run(async () => await _categoryRepository.UpdateCategoryAsync(category));
    }

    public Task<(string, int)> DeleteCategoryAsync(Category category)
    {
        IsDirty = true;
        return Task.Run(async () => await _categoryRepository.DeleteCategoryAsync(category));
    }
}
