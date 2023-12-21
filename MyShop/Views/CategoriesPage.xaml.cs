using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class CategoriesPage : Page
{
    public CategoriesViewModel ViewModel
    {
        get;
    }

    public CategoriesPage()
    {
        ViewModel = App.GetService<CategoriesViewModel>();
        InitializeComponent();
    }
}
