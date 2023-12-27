using System.Collections.ObjectModel;

using CommunityToolkit.Mvvm.ComponentModel;

using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.ViewModels;

public partial class CategoryViewModel : ObservableRecipient, INavigationAware
{
    private readonly ICategoryDataService _categoryDataService;

    [ObservableProperty]
    private Category? selected;

    [ObservableProperty]
    public bool isLoading = true;

    [ObservableProperty]
    public bool isContentReady = false;

    public ObservableCollection<Category> CategoryList { get; private set; } = new();

    public CategoryViewModel(ICategoryDataService categoryDataService)
    {
        _categoryDataService = categoryDataService;
    }


    public async void LoadCategories()
    {
        await Task.Run(async () => await _categoryDataService.LoadDataAsync());
        var (categories, _, _) = _categoryDataService.GetData();

        if (categories is not null)
        {
            foreach (var category in categories)
            {
                CategoryList.Add(category);
            }
        }

        IsLoading = false;
        IsContentReady = true;
    }

    public void OnNavigatedTo(object parameter)
    {
        LoadCategories();
        EnsureItemSelected();
    }

    public void OnNavigatedFrom()
    {
    }

    public void EnsureItemSelected()
    {
        if (CategoryList.Count > 0)
        {
            Selected ??= CategoryList[0];
        }
    }
}
