using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class BooksPage : Page
{
    public BooksViewModel ViewModel
    {
        get;
    }

    public BooksPage()
    {
        ViewModel = App.GetService<BooksViewModel>();
        InitializeComponent();
    }
}
