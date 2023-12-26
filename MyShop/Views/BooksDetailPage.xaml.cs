using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

using MyShop.Contracts.Services;
using MyShop.Core.Models;
using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class BooksDetailPage : Page
{
    public BooksDetailViewModel ViewModel
    {
        get;
    }

    public BooksDetailPage()
    {
        ViewModel = App.GetService<BooksDetailViewModel>();
        InitializeComponent();
        Loaded += UpdateVisualState;
        SizeChanged += UpdateVisualState;
    }

    // Responsive 

    private void UpdateVisualState(object sender, RoutedEventArgs e)
    {
        var windowWidth = App.MainWindow.Width;

        if (windowWidth < 960)
        {
            DetailPanel.Orientation = Orientation.Vertical;
            EditBookStackPanel.MinWidth = 300;
        }
        else
        {
            DetailPanel.Orientation = Orientation.Horizontal;
            EditBookStackPanel.MinWidth = 600;
        }
    }


    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
    }

    protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
    {
        base.OnNavigatingFrom(e);
        if (e.NavigationMode == NavigationMode.Back)
        {
            var navigationService = App.GetService<INavigationService>();

            if (ViewModel.Item != null)
            {
                navigationService.SetListDataItemForNextConnectedAnimation(ViewModel.Item);
            }
        }
    }

    private async void DeleteItemButton_Click(object sender, RoutedEventArgs e)
    {
        var deleteFileDialog = new ContentDialog
        {
            Title = "Delete book permanently?",
            Content = "If you delete this book, you won't be able to recover it. Do you want to delete it?",
            PrimaryButtonText = "Delete",
            CloseButtonText = "Cancel",
            XamlRoot = DetailPanel.XamlRoot
        };
        var result = await deleteFileDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            ViewModel.DeleteBook();
        }
    }

    private void CategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var category = (sender as ComboBox)?.SelectedItem as Category;

        if (category is not null)
        {
            ViewModel.EditBook.CategoryId = category.Id;
        }
    }
}
