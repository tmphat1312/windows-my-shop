using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using MyShop.Core.Http;
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
        Loaded += UpdateVisualState;
        SizeChanged += UpdateVisualState;
    }

    private void UpdateVisualState(object sender, RoutedEventArgs e)
    {
        var windowWidth = App.MainWindow.Width;

        // VisualStateManager sucks

        if (windowWidth < 780)
        {
            FiltersAndSearchPanel.Visibility = Visibility.Collapsed;
            SmallFiltersAndSearchPanel.Visibility = Visibility.Visible;
        }
        else
        {
            FiltersAndSearchPanel.Visibility = Visibility.Visible;
            SmallFiltersAndSearchPanel.Visibility = Visibility.Collapsed;
        }
    }

    private void SortByComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedItem = (sender as ComboBox)?.SelectedItem as HttpSortObject;

        if (selectedItem is not null)
        {
            ViewModel.SelectSortOption(selectedItem);
        }
    }
}
