using Microsoft.UI.Xaml.Controls;
using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class AddBookPage : Page
{
    public AddBookViewModel ViewModel
    {
        get;
    }

    public AddBookPage()
    {
        ViewModel = App.GetService<AddBookViewModel>();
        InitializeComponent();
    }
}
