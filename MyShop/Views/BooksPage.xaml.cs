using Microsoft.UI.Xaml;
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
        Loaded += BooksPage_Loaded;
        SizeChanged += BooksPage_SizeChanged;
    }


    private void BooksPage_Loaded(object sender, RoutedEventArgs e)
    {
        UpdateVisualState();
    }

    private void BooksPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        UpdateVisualState();
    }

    private void UpdateVisualState()
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
}
