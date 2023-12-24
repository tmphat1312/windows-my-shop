using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class AddUserPage : Page
{
    public AddUserViewModel ViewModel
    {
        get;
    }

    public AddUserPage()
    {
        ViewModel = App.GetService<AddUserViewModel>();
        InitializeComponent();
    }
}
