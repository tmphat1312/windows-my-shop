using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Services;

namespace MyShop.ViewModels;

public partial class BooksViewModel : ResourceLoadingViewModel, INavigationAware
{
    private readonly INavigationService _navigationService;
    private readonly IBookDataService _bookDataService;
    private readonly IResourcePagingService _resourcePagingService;
    public IResourcePagingService ResourcePagingService => _resourcePagingService;

    public ObservableCollection<Book> Source { get; } = new ObservableCollection<Book>();

    public BooksViewModel(INavigationService navigationService, IBookDataService bookDataService, IResourcePagingService resourcePagingService)
    {
        _navigationService = navigationService;
        _bookDataService = bookDataService;
        _resourcePagingService = resourcePagingService;
    }

    public async void OnNavigatedTo(object parameter)
    {
        if (Source.Count == 0)
        {
            IsLoading = true;
            IsReady = !IsLoading;
            ErrorMessage = string.Empty;
            InfoMessage = string.Empty;

            Source.Clear();

            var (data, totalItems, message, ERROR_CODE) = await Task.Run(async () =>
            {
                var data = await _bookDataService.LoadBookAsync();
                return data;
            });

            if (data is not null)
            {
                foreach (var item in data)
                {
                    Source.Add(item);
                }

                IsLoading = false;
                IsReady = !IsLoading;
                TotalItems = totalItems;
            }
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
}
