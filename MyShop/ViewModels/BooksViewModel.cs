using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using MyShop.Contracts.Services;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;

namespace MyShop.ViewModels;

public partial class BooksViewModel : ObservableRecipient, INavigationAware
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
        Source.Clear();

        var data = await _bookDataService.GetContentGridDataAsync();

        foreach (var item in data)
        {
            Source.Add(item);
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
