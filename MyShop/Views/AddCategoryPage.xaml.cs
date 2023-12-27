using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class AddCategoryPage : Page
{
    public AddCategoryViewModel ViewModel
    {
        get;
    }

    public AddCategoryPage()
    {
        ViewModel = App.GetService<AddCategoryViewModel>();
        InitializeComponent();
    }
}
