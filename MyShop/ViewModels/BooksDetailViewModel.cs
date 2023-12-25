using System.Collections.ObjectModel;
using MyShop.Contracts.ViewModels;
using MyShop.Core.Contracts.Services;
using MyShop.Core.Models;
using MyShop.Services;

namespace MyShop.ViewModels;

public partial class BooksDetailViewModel : ResourceLoadingViewModel, INavigationAware
{
    private readonly IReviewDataService _reviewDataService;

    public ObservableCollection<Review> Source { get; } = new ObservableCollection<Review>();

    public BooksDetailViewModel(IReviewDataService reviewDataService)
    {
        _reviewDataService = reviewDataService;
    }

    public Book? Item
    {
        get; set;
    }

    public async void LoadReviewsAsync()
    {
        IsLoading = true;
        InfoMessage = string.Empty;
        ErrorMessage = string.Empty;
        NotfifyChanges();

        await Task.Run(async () => await _reviewDataService.LoadDataAsync());

        var (data, totalItems, message, ERROR_CODE) = _reviewDataService.GetData();

        if (data is not null)
        {
            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.Content))
                {
                    item.Content = "No review content";
                }
                Source.Add(item);
            }

            IsLoading = false;
            TotalItems = totalItems;

            if (TotalItems == 0)
            {
                InfoMessage = "This book hasn't had any reviews";
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
        if (parameter is Book book)
        {
            Item = book;
            _reviewDataService.BookId = Item?.Id ?? string.Empty;
            LoadReviewsAsync();
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
