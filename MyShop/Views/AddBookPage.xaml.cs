using Microsoft.UI.Xaml.Controls;
using MyShop.Core.Models;
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

    private void CategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var category = (sender as ComboBox)?.SelectedItem as Category;

        if (category is not null)
        {
            ViewModel.NewBook.CategoryId = category.Id;
        }
    }
}
