using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.Extensions.Hosting;
using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Http;
using MyShop.Core.Models;
using MyShop.Core.Services;
using MyShop.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace MyShop.ViewModels;

public partial class AddOrderViewModel : ResourceLoadingViewModel, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IBookDataService _bookDataService;
    private readonly IOrderDataService _oderDataService;
    private readonly ICategoryDataService _categoryDataService;

    public ObservableCollection<Book> Source { get; } = new ObservableCollection<Book>();

    public RelayCommand AddOrderCommand
    {
        get;
    }

    public AddOrderViewModel(INavigationService navigationService, IBookDataService bookDataService, IOrderDataService orderDataService, ICategoryDataService categoryDataService)
    {
        AddOrderCommand = new RelayCommand(OnAddOrder);

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

    private async void OnAddOrder()
    {
        var validBooks = Source.Where(b => b.OrderQuantity > 0).ToList();

        if(validBooks.Count == 0)
        {

            ErrorMessage = "Please select at least one book";
            NotfifyChanges();
            await Task.Delay(2000); // Chờ 2 giây
            ErrorMessage = string.Empty;
            NotfifyChanges();
            return;
        }
        else
        {
            var orderDetails = validBooks.Select(book => new AddOrderDetail
            {
                BookId = book.Id,
                Quantity = book.OrderQuantity, // Lấy số lượng mua
                Price = book.SellingPrice // Lấy giá của sách
            }).ToList();

            var result =  await Task.Run(async () => await _oderDataService.CreateAOrderAsync(orderDetails));

            int errorCode = result.Item3;
            if (errorCode == 0)
            {
                _navigationService.NavigateTo("MyShop.ViewModels.OrdersViewModel");
            }
            else
            {
                ErrorMessage = result.Item2;
                NotfifyChanges();
                await Task.Delay(2000); // Chờ 2 giây
                ErrorMessage = string.Empty;
                NotfifyChanges();
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

        _bookDataService.SearchParams = BuildSearchParams();
        _bookDataService.IsDirty = true;

        await Task.Run(async () => await _bookDataService.LoadDataAsync());

        var (data, totalItems, message, ERROR_CODE) = _bookDataService.GetData();

        if (data is not null)
        {
            Source.Clear();

            foreach (var item in data)
            {
                Source.Add(item);
            }

            IsLoading = false;
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
    private void OnApplyFiltersAndSearch()
    {
        LoadData();
    }

   
}
