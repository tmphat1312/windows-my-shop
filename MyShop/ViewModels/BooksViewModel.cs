﻿using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Http;
using MyShop.Core.Models;
using MyShop.Services;

namespace MyShop.ViewModels;

public partial class BooksViewModel : ResourceLoadingViewModel, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IBookDataService _bookDataService;
    private readonly ICategoryDataService _categoryDataService;

    public ObservableCollection<Book> Source { get; } = new ObservableCollection<Book>();

    public BooksViewModel(INavigationService navigationService, IBookDataService bookDataService, ICategoryDataService categoryDataService, IStorePageSettingsService storePageSettingsService) : base(storePageSettingsService)
    {
        _navigationService = navigationService;
        _bookDataService = bookDataService;
        _categoryDataService = categoryDataService;
        FunctionOnCommand = LoadData;

        SortOptions = new List<HttpSortObject>
        {
            new() { Name = "Default", Value = "default", IsAscending = true },
            new() { Name = "Title (A-Z)", Value = "name", IsAscending = true },
            new() { Name = "Title (Z-A)", Value="name", IsAscending = false },
            new() { Name = "Price (Low - High)", Value="sellingPrice", IsAscending = true },
            new() { Name = "Price (High - Low)", Value="sellingPrice", IsAscending = false },
            new() { Name = "PublishYear (Past)", Value="publishedYear", IsAscending = true },
            new() { Name = "PublishYear (Recent)", Value="publishedYear", IsAscending = false },
        };
        SelectedSortOption = SortOptions[0];
    }

    public async void LoadCategories()
    {
        await Task.Run(async () => await _categoryDataService.LoadDataAsync());
        var (categories, _, _) = _categoryDataService.GetData();

        if (categories is not null)
        {
            foreach (var category in categories)
            {
                CategoryFilters.Add(category);
            }
        }

    }

    public async void LoadData()
    {
        IsDirty = false;
        IsLoading = true;
        InfoMessage = string.Empty;
        ErrorMessage = string.Empty;
        NotfifyChanges();

        _bookDataService.SearchParams = await BuildSearchParamsAsync();

        await Task.Run(async () => await _bookDataService.LoadDataAsync());

        var (data, totalItems, message, ERROR_CODE) = _bookDataService.GetData();

        if (data is not null)
        {
            Source.Clear();

            foreach (var item in data)
            {
                Source.Add(item);
            }

            TotalItems = totalItems;

            if (TotalItems == 0)
            {
                InfoMessage = "No books found";
            }
        }
        else
        {
            if (ERROR_CODE != 0)
            {
                ErrorMessage = message;
            }
        }

        IsLoading = false;
        NotfifyChanges();
    }

    public void OnNavigatedTo(object parameter)
    {
        if (Source.Count <= 0)
        {
            LoadData();
            LoadCategories();
        }
    }

    public void OnNavigatedFrom()
    {
    }

    [RelayCommand]
    private void OnItemClick(Book? clickedItem)
    {
        if (clickedItem != null)
        {
            _navigationService.SetListDataItemForNextConnectedAnimation(clickedItem);
            _navigationService.NavigateTo(typeof(BooksDetailViewModel).FullName!, clickedItem);
        }
    }

    [RelayCommand]
    private void OnApplyFiltersAndSearch()
    {
        LoadData();
    }
}
