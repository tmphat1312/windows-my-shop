using Microsoft.UI.Xaml.Controls;

using MyShop.ViewModels;

namespace MyShop.Views;

public sealed partial class ImportDataPage : Page
{
    public ImportDataViewModel ViewModel
    {
        get;
    }

    public ImportDataPage()
    {
        ViewModel = App.GetService<ImportDataViewModel>();
        InitializeComponent();
    }
}
