using CommunityToolkit.WinUI.UI.Controls;

using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class CategoryPage : Page
{
    public CategoryViewModel ViewModel
    {
        get;
    }

    public CategoryPage()
    {
        ViewModel = App.GetService<CategoryViewModel>();
        InitializeComponent();
    }

    private void OnViewStateChanged(object sender, ListDetailsViewState e)
    {
        if (e == ListDetailsViewState.Both)
        {
            ViewModel.EnsureItemSelected();
        }
    }
}
