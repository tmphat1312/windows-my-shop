using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class SettingsPage : Page
{
    public SettingsViewModel ViewModel
    {
        get;
    }

    public SettingsPage()
    {
        ViewModel = App.GetService<SettingsViewModel>();
        InitializeComponent();
    }

    private void ItemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        ViewModel.SaveItemsPerPage();
    }

    private void OpenLastPageCheckbox_Checked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.SaveOpenLastPage();
    }

    private void OpenLastPageCheckbox_Unchecked(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {
        ViewModel.SaveNotOpenLastPage();
    }
}
