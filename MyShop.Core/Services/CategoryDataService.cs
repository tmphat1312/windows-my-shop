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

    public CategoryDataService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public (IEnumerable<Category>, string, int) GetData() => _categoryTuple;

    public async Task<(IEnumerable<Category>, string, int)> LoadDataAsync()
    {
        if (!IsAlreadyFetched)
        {
            _categoryTuple = ((List<Category>, string, int))await _categoryRepository.GetCategoriesAsync();
            IsAlreadyFetched = true;
        }

        return _categoryTuple;
    }

    public Task<(Category, string, int)> AddCategoryAsync(Category category)
    {
        return Task.Run(async () => await _categoryRepository.CreateCategoryAsync(category));
    }
}
