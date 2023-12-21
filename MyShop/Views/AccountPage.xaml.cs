using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class AccountPage : Page
{
    public AccountViewModel ViewModel
    {
        get;
    }

    public AccountPage()
    {
        ViewModel = App.GetService<AccountViewModel>();
        InitializeComponent();
    }
}
