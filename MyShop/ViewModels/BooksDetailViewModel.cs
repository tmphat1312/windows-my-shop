using CommunityToolkit.Mvvm.ComponentModel;

using MyShop.Contracts.ViewModels;
using MyShop.Core.Models;

namespace MyShop.ViewModels;

public partial class BooksDetailViewModel : ObservableRecipient, INavigationAware
{
    public Book? Item
    {
        get; set;
    }

    public BooksDetailViewModel()
    {
    }

    public void OnNavigatedTo(object parameter)
    {
        if (parameter is Book book)
        {
            Item = book;
        }
    }

    public void OnNavigatedFrom()
    {
    }
}
